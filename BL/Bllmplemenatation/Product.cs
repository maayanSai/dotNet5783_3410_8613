﻿namespace BlImplementation;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using System.Runtime.CompilerServices;

/// <summary>
/// A logical entity-item
/// </summary>
internal class Product : IProduct
{
    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;

    /// <summary>
    /// Adding a product (for the manager screen)
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="BO.Exceptions.BlInvalidInputException"></exception>
    /// <exception cref="Exceptions.BlInvalidInputException"></exception>
    /// <exception cref="Exceptions.BOAlreadyExistsException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(BO.Product p)
    {
        if (p.ID <= 0) // if id is negative
            throw new BO.BlInCorrectException("product ID");
        if (p.InStock < 0) //negative amount
            throw new BO.BlInCorrectException("product amount");
        if (p.Price < 0) //negative price
            throw new BO.BlInCorrectException("product price");
        if (p.Name == "") //empty string
            throw new BO.BlInCorrectException("product name");
        try
        {
            DO.Product p1 = new()
            {
               ID = p.ID,
                Name = p?.Name ?? throw new BO.BlNullPropertyException("Empty name"),
                Price = p?.Price ?? 0,
                Category = (DO.Category?)p?.Category ?? throw new BO.BlWorngCategoryException("Wrong category "),
                InStock = p.InStock,
                ImageRelativeName = p?.ImageRelativeName,
            };
            lock (dal)
            {
                dal?.Product.Add(p1);
            }
        }
        catch (DO.DalAlreadyExistsException exp)
        {
            throw new BO.BlAlreadyExistEntityException("The pruduct is allredy exssists", exp);
        }
    }

    /// <summary>
    /// Product deletion (for admin screen)
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlInCorrectException"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BlMissingEntityException"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        if (id <= 0) // if id is negative
            throw new BO.BlInCorrectException("negative id");
        lock (dal)
        {
            if (dal!.OrderItem.GetAll().Where(orderItem => orderItem?.ProductID == id).Any())
                throw new BO.BlAlreadyExistEntityException("the product is exsist in order/s so that can not be deleated");
        }
        try
        {
            lock (dal)
            {
                dal?.Product.Delete(id);
            }
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException("The product doesnt exsist", exp);//לשנות לסוג אקספשיון הנדרש
        }
    }

    /// <summary>
    /// Product list request (for manager screen and buyer's catalog screen)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exceptions.BlNullPropertyException"></exception>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList?> GetProducts(Func<BO.ProductForList?, bool>? func = null)
    {
        IEnumerable<BO.ProductForList?> pro = from DO.Product item in dal!.Product.GetAll()
                                              select new BO.ProductForList()
                                              {
                                                  ID = item.ID,
                                                  Name = item.Name,
                                                  Price = item.Price,
                                                  Category = (BO.Category?)item.Category,
                                                  ImageRelativeName=item.ImageRelativeName
                                              };
        return func is null ? pro : pro.Where(func);
    }

    /// <summary>
    /// Product details request (for admin screen and for)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlInCorrectException"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BlWorngCategoryException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product ItemProduct(int id)
    {
        if (id <= 0)
            throw new BO.BlInCorrectException("inncorrect id");
        DO.Product? dopro;
        try
        {
            lock (dal)
            {
                dopro = dal?.Product.GetById(id);
            }
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException("product does not exsist", exp);
        }
        BO.Product bopro = new()
        {
            ID = dopro?.ID ?? throw new BO.BlNullPropertyException("Null priduct id"),
            Name = dopro?.Name ?? "",
            Price = dopro?.Price ?? 0,
            Category = (BO.Category?)dopro?.Category ?? throw new BO.BlWorngCategoryException("Wrong product category"),
            InStock = dopro?.InStock ?? 0,
            ImageRelativeName=dopro?.ImageRelativeName,
        };
        return bopro;
    }
    /// <summary>
    /// Product details request (for buyer screen - from the catalog)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlInCorrectException"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    
    public BO.ProductItem ItemProduct(int id, BO.Cart cart)
    {
        if (id <= 0)
            throw new BO.BlInCorrectException("incorrct id");
        DO.Product? p;
        try
        {
            lock (dal)
            {
                p = dal?.Product.GetById(id);
            }
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException(exp.Message);
        }
        int amount = 0;
        if (cart.Items != null)
        {
            BO.OrderItem? itemIn = cart.Items?.FirstOrDefault(item => item?.ProductID == id); //if items in cart null error in runtime
            if (itemIn != null)
                amount = itemIn.Amount;
        }
        BO.ProductItem proi = new()
        {
            ID = p?.ID ?? throw new BO.BlNullPropertyException("Null product id"),
            Name = p?.Name ?? "",
            Price = p?.Price ?? 0,
            Amount = amount,
            isStock=p?.InStock>0,
            Category=(BO.Category?)p?.Category!,
            ImageRelativeName=p?.ImageRelativeName
        };
        return proi;
    }

    /// <summary>
    /// Update product data (for admin screen)
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="BO.BlInCorrectException"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BlMissingEntityException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(BO.Product p)
    {
        //input validation
        if (p.ID <= 0) //negative id
            throw new BO.BlInCorrectException("product ID");
        if (p.InStock < 0) //negative amount
            throw new BO.BlInCorrectException("product amount");
        if (p.Price < 0) //negative price
            throw new BO.BlInCorrectException("product price");
        if (p.Name == "") //empty string
            throw new BO.BlInCorrectException("product name");
        try
        {
            DO.Product dp = new()
            {
                ID = p.ID,
                Name = p?.Name ?? "",
                Price = p?.Price ?? 0,
                InStock = p?.InStock ?? 0,
                Category = (DO.Category?)p?.Category ?? throw new BO.BlInCorrectException("product name"),
                ImageRelativeName = p.ImageRelativeName,
            };
            lock (dal)
            {
                dal?.Product.Update(dp);
            }
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException("the product dosnt exsist", exp);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList?> GetListedProductByCategory(BO.Category c)
    {
        return GetProducts().Where(x => x?.Category == c);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem?> GetProductItem(BO.Cart cart,Func<BO.ProductItem,bool>?fanc=null)
    {
        IEnumerable<BO.ProductItem?> pro;
        if (fanc==null)
        {
            lock (dal)
            {
                pro = from DO.Product item in dal!.Product.GetAll()
                      select new BO.ProductItem()
                      {
                          ID = item.ID,
                          Name = item.Name,
                          Price = item.Price,
                          Category = (BO.Category?)item.Category,
                          Amount = cart?.Items?.FirstOrDefault(x => x?.ProductID == item.ID)?.Amount ?? 0,
                          isStock = item.InStock > 0,
                          ImageRelativeName = item.ImageRelativeName,
                      };
            }

        }
        else
        {
            pro = from DO.Product item in dal!.Product.GetAll()
                  select new BO.ProductItem()
                  {
                      ID = item.ID,
                      Name = item.Name,
                      Price = item.Price,
                      Category = (BO.Category?)item.Category,
                      Amount=cart?.Items?.FirstOrDefault(x => x?.ProductID==item.ID)?.Amount??0,
                      isStock=item.InStock>0,
                      ImageRelativeName=item.ImageRelativeName,
                  };
            pro=pro.Where(fanc!);
        }
        return pro;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductForList? GetProduct(int id) 
   {
        try
        {
            var p = GetProducts(x => x?.ID == id).First();
            return p;
        }
        catch(BO.BlInCorrectException)
        {
            throw new BO.BlInCorrectException("incorrect id");
        }
    }
}
