namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class Product:IProduct
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
        if (p.ID <= 0)//negative id
            throw new BO.Exceptions.BlInvalidInputException("product ID");
            if(p.InStock< 0)//negative amount
                throw new BO.Exceptions.BlInvalidInputException("product amount");
            if (p.Price< 0)//negative price
                throw new BO.Exceptions.BlInvalidInputException("product price");
            if (p.Name == "")//empty string
                throw new BO.Exceptions.BlInvalidInputException("product name");
        try
        {
            DO.Product p1 = new()
            {
                ID = p.ID,
                Name=p?.Name?? throw new Exceptions.BlInvalidInputException("missing product name"),//לשנות לסוג אקספשיון הנדרש
                Price=p.Price,
                Category=(DO.Category)p.Category,//צריך לבדוק אם הומר תקין?
                InStock=p.InStock,
            };
        }
        catch (DO.DalAlreadyExistsException exp)
        {
                  throw new Exceptions.BOAlreadyExistsException(exp.Message);//לשנות לסוג אקספשיון הנדרש
        }
}
    /// <summary>
    /// Product deletion (for manager screen)
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exceptions.BlInvalidInputException"></exception>
    /// <exception cref="Exceptions.BODoesNotExistException"></exception>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        if (id<=0)
            throw new Exceptions.BlInvalidInputException("negative id");
            bool exist = false;
        
        IEnumerable<DO.Order?> orders = new List<DO.Order?>(Dal.Order.GetAll());
        foreach (var ord in orders)
        { 
            IEnumerable<DO.OrderItem?> ordi = new List<DO.OrderItem?>();
            ordi=Dal.OrderItem.GetByOrderId(ord?.ID??throw new Exceptions.BlInvalidInputException("order id is missing"));
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
            catch (DalDoesNotExistException exp)
            {
                throw new Exceptions.BODoesNotExistException(exp.Message);//לשנות לסוג אקספשיון הנדרש
            }
        }
        else
            throw new Exception("the product is exsist in order/s so that can not be deleated");//לשנות לסוג אקספשיון הנדרש

    }
    /// <summary>
    /// Product list request (for manager screen and buyer's catalog screen)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exceptions.BlNullPropertyException"></exception>
    /// <exception cref="Exception"></exception>
    public IEnumerable<ProductForList?> GetProducts()
    {
        IEnumerable<DO.Product?> pro=Dal.Product.GetAll();

        return pro.Select(pr => new BO.ProductForList
        {
            ID = pr?.ID??throw new Exceptions.BlNullPropertyException("missing id"),
            Name = pr?.Name??throw new Exceptions.BlNullPropertyException("missing name"),
            Price = pr?.Price??0,
            Category = (BO.Category?)pr?.Category??throw new Exception("wrong cayegory"),
        });
        
       
        
    }

    public BO.Product ItemProduct(int id)
    {
        if(id<=0)
        {
            throw new Exceptions.BlInvalidInputException("inncorrect id");
        }
        DO.Product dopro;
        try
        {
            dopro = Dal.Product.GetById(id);
        }
        catch (DO.DalDoesNotExistException exp)
        {
            throw new Exception(exp.Message); //לשנות לסוג אקספשיון הנדרש
        }
       BO.Product bopro = new()
        {
            ID = dopro.ID,
            Name = dopro.Name,
            Price = dopro.Price,
            Category = (BO.Category)dopro.Category,
            InStock = dopro.InStock,//??
        };
        return bopro;
    }

    public BO.ProductItem ItemProduct(int id, BO.Cart cart)
    {
        if(id<=0)
        {
            throw new Exceptions.BlInvalidInputException("incorrct id");
          
        }
        DO.Product p;
        try
        {
            p = Dal.Product.GetById(id);
        }
        catch (DO.DalDoesNotExistException exp)
        {
            throw new Exceptions.BODoesNotExistException(exp.Message);
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
            ID = p.ID,
            Name = p.Name,
            Price = p.Price,
            isStock = p.InStock > 0 ,
            AmountCart = amount ,//לשנות!
        };
        return proi;
        
    }

    public void Update(BO.Product p)
    {
        //input validation
        if (p.ID <= 0)//negative id
            throw new Exceptions.BlInvalidInputException("product ID");
        if (p.InStock < 0)//negative amount
            throw new Exceptions.BlInvalidInputException("product amount");
        if (p.Price < 0)//negative price
            throw new Exceptions.BlInvalidInputException("product price");
        if (p.Name == "")//empty string
            throw new Exceptions.BlInvalidInputException("product name");

        try
        {
            DO.Product dp = new()
            {
                ID = p.ID,
                Name = p.Name ?? throw new Exceptions.BlInvalidInputException("product name"),
                Price = p.Price,
                InStock = p.InStock,
                Category = (DO.Category)p.Category
            };
            Dal.Product.Update(dp);
        }
         catch (DO.DalDoesNotExistException exp)
        {
            throw new Exceptions.BODoesNotExistException(exp.Message);
        }
 
    }
}
