﻿namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A logical entity-item
/// </summary>
internal class Product : IProduct
{
    DalApi.IDal Dal = new Dal.DalList();

    /// <summary>
    /// Adding a product (for the manager screen)
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="BO.Exceptions.BlInvalidInputException"></exception>
    /// <exception cref="Exceptions.BlInvalidInputException"></exception>
    /// <exception cref="Exceptions.BOAlreadyExistsException"></exception>
    public void Add(BO.Product p)
    {
        if (p.ID <= 0) // if id is negative
            throw new BO.BlInCorrectException("product ID");
        if (p.InStock< 0) //negative amount
            throw new BO.BlInCorrectException("product amount");
        if (p.Price< 0) //negative price
            throw new BO.BlInCorrectException("product price");
        if (p.Name == "") //empty string
            throw new BO.BlInCorrectException("product name");
        try
        {
            DO.Product p1 = new()
            {
                ID = p.ID,
                Name=p?.Name??throw new BO.BlNullPropertyException("Empty name"),
                Price=p?.Price??0,
                Category=(DO.Category?)p?.Category ?? throw new BO.BlWorngCategoryException("Wrong category "),
                InStock=p.InStock,
            };
            Dal.Product.Add(p1);
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
    public void Delete(int id)
    {
        if (id<=0) // if id is negative
            throw new BO.BlInCorrectException("negative id");


        if (Dal.OrderItem.GetAll().Where(orderItem => orderItem?.ProductID==id).Any())
            throw new BO.BlAlreadyExistEntityException("the product is exsist in order/s so that can not be deleated");
            try
            {
                Dal.Product.Delete(id);
            }
            catch (DalMissingIdException exp)
            {
                throw new BlMissingEntityException("The product doesnt exsist", exp);//לשנות לסוג אקספשיון הנדרש
            }
        
    
    }
    /// <summary>
    /// Product list request (for manager screen and buyer's catalog screen)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exceptions.BlNullPropertyException"></exception>
    /// <exception cref="Exception"></exception>
    public IEnumerable<ProductForList?> GetProducts()
    {
        IEnumerable<DO.Product?> pro = Dal.Product.GetAll();
        return pro.Select(pr => new BO.ProductForList
        {
            ID = pr?.ID ?? throw new BO.BlNullPropertyException("null product id"),
            Name = pr?.Name ?? "",
            Price = pr?.Price ?? 0,
            Category = (BO.Category?)pr?.Category ?? throw new BO.BlWorngCategoryException("wrong product cayegory"),
        });
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
    public BO.Product ItemProduct(int id)
    {
        if (id<=0)
            throw new BO.BlInCorrectException("inncorrect id");
        DO.Product? dopro;
        try
        {
            dopro = Dal.Product.GetById(id);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException("product des not exsist", exp);
        }
        BO.Product bopro = new()
        {
            ID = dopro?.ID ?? throw new BO.BlNullPropertyException("Null priduct id"),
            Name = dopro?.Name ?? "",
            Price = dopro?.Price ?? 0,
            Category = (BO.Category?)dopro?.Category ??throw new BlWorngCategoryException("Wrong product category"),
            InStock = dopro?.InStock ?? 0,
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
    public BO.ProductItem ItemProduct(int id, BO.Cart cart)
    {
        if (id<=0)
            throw new BO.BlInCorrectException("incorrct id");
        DO.Product? p;
        try
        {
            p = Dal.Product.GetById(id);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException(exp.Message);
        }
        int amount = 0;
        if (cart.Items != null)
        {
            BO.OrderItem? itemIn = cart.Items?.FirstOrDefault(item => item.ProductID == id); //if items in cart null error in runtime
            if (itemIn != null)
                amount = itemIn.Amount;
        }
        BO.ProductItem proi = new()
        {
            ID = p?.ID?? throw new BO.BlNullPropertyException("Null product id"),
            Name = p?.Name??"",
            Price = p?.Price??0,
            isStock = p?.InStock > 0,
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
                Name = p?.Name ??"",
                Price = p?.Price??0,
                InStock = p?.InStock??0,
                Category = (DO.Category?)p?.Category?? throw new BO.BlInCorrectException("product name"),
            };
            Dal.Product.Update(dp);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BlMissingEntityException("the product dosnt exsist", exp);
        }
    }
}
