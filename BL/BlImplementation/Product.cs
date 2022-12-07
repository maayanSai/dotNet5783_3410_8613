

namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

internal class Product:IProduct
{
    DalApi.IDal Dal = new Dal.DalList();

    public void Add(BO.Product p)
    {
        if (p.ID <= 0)//negative id
            throw new BO.BlInCorrectException("product ID");
            if(p.InStock< 0)//negative amount
                throw new BO.BlInCorrectException("product amount");
            if (p.Price< 0)//negative price
                throw new BO.BlInCorrectException("product price");
            if (p.Name == "")//empty string
                throw new BO.BlInCorrectException("product name");
        try
        {
            DO.Product p1 = new()
            {
                ID = p.ID,
                Name=p?.Name??throw new BO.BlNullPropertyException("Empty name"),
                Price=p.Price,
                Category=(DO.Category?)p?.Category ?? throw new BO.BlWorngCategoryException("Wrong category "),//צריך לבדוק אם הומר תקין?
                InStock=p.InStock,
            };
            Dal.Product.Add(p1);
        }
        catch (DO.DalAlreadyExistsException exp)
              {
                  throw new BO.BlAlreadyExistEntityException("The pruduct is allredy exssists",exp);//לשנות לסוג אקספשיון הנדרש
             }
     
        }

     public void Delete(int id)
    {
        if (id<=0)
            throw new BO.BlInCorrectException("negative id");
            bool exist = false;
        
        IEnumerable<DO.Order?> orders = new List<DO.Order?>(Dal.Order.GetAll());
        foreach (var ord in orders)
        { 
            IEnumerable<DO.OrderItem?> ordi = new List<DO.OrderItem?>();
            ordi=Dal.OrderItem.GetByOrderId(ord?.ID??throw new BO.BlNullPropertyException("order id is missing"));
            foreach (var oi in ordi)
            {
                if (oi?.ID==id)
                    exist=true;
            }
        }
        if (!exist)
        {
            try
            {
                Dal.Product.Delete(id);
            }
            catch (DalMissingIdException exp)
            {
                throw new BlMissingEntityException("The product doesnt exsist",exp);//לשנות לסוג אקספשיון הנדרש
            }
        }
        else
            throw new BO.BlMissingEntityException("the product is exsist in order/s so that can not be deleated");//לשנות לסוג אקספשיון הנדרש

    }
        
   

    public IEnumerable<ProductForList?> GetProducts()
    {
        IEnumerable<DO.Product?> pro=Dal.Product.GetAll();

        return pro.Select(pr => new BO.ProductForList
        {
            ID = pr?.ID ?? throw new BO.BlNullPropertyException("null product id"),
            Name = pr?.Name ?? "",
            Price = pr?.Price ?? 0,
            Category = (BO.Category?)pr?.Category ?? throw new BO.BlWorngCategoryException("wrong product cayegory"),

        }) ;
        
       
        
    }

    public BO.Product ItemProduct(int id)
    {
        if(id<=0)
        {
            throw new BO.BlInCorrectException("inncorrect id");
        }
        DO.Product? dopro;
        try
        {
            dopro = Dal.Product.GetById(id);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException("product des not exsist",exp); //לשנות לסוג אקספשיון הנדרש
        }
        BO.Product bopro = new()
        {
            ID = dopro?.ID ?? throw new BO.BlNullPropertyException("Null priduct id"),
            Name = dopro?.Name ?? "",
            Price = dopro?.Price ?? 0,
            Category = (BO.Category?)dopro?.Category ??throw new BlWorngCategoryException("Wrong product category"),
            InStock = dopro?.InStock??0,//??
        };
        return bopro;
    }

    public BO.ProductItem ItemProduct(int id, BO.Cart cart)
    {
        if(id<=0)
        {
            throw new BO.BlInCorrectException("incorrct id");
          
        }
        DO.Product? p;
        try
        {
            p = Dal.Product.GetById(id);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException(exp.Message);
        }
        //לחזור
        int amount=0;
        if (cart.Items != null)
        {
            BO.OrderItem? itemIn = cart.Items?.FirstOrDefault(item => item.ProductID == id);//if items in cart null error in runtime!!!!

            if (itemIn != null)
                amount = itemIn.Amount;

        }
        BO.ProductItem proi = new()
        {
            ID = p?.ID?? throw new BO.BlNullPropertyException("Null product id"),
            Name = p?.Name??"",
            Price = p?.Price??0,
            isStock = p?.InStock > 0 ,
            AmountCart = amount ,
        };
        return proi;
        
    }

    public void Update(BO.Product p)
    {
        //input validation
        if (p.ID <= 0)//negative id
            throw new BO.BlInCorrectException("product ID");
        if (p.InStock < 0)//negative amount
            throw new BO.BlInCorrectException("product amount");
        if (p.Price < 0)//negative price
            throw new BO.BlInCorrectException("product price");
        if (p.Name == "")//empty string
            throw new BO.BlInCorrectException("product name");

        try
        {
            DO.Product dp = new()
            {
                ID = p?.ID ?? throw new BO.BlNullPropertyException("product name"),
                Name = p.Name ??"" ,
                Price = p.Price,
                InStock = p.InStock,
                Category = (DO.Category?)p?.Category?? throw new BO.BlInCorrectException("product name"),
            };
            Dal.Product.Update(dp);
        }
         catch (DO.DalMissingIdException exp)
        {
            throw new BlMissingEntityException("the product dosnt exsist",exp);
        }
 
    }
}
