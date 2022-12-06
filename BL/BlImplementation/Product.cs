

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
        if (p.ID>0&&p.Name!=""&&p.Price>0&&p.InStock>0)
        {
            DO.Product p1 = new()
            {
                ID = p.ID,
                Name=p?.Name??throw new Exception("missing name"),//לשנות לסוג אקספשיון הנדרש
                Price=p?.Price??throw new Exception("missing price"),
                Category=(DO.Category)p.Category,//צריך לבדוק אם הומר תקין?
                InStock=p?.InStock??throw new Exception("missing in stok"),
            };
            try
            {
                Dal.Product.Add(p1);
            }
            catch (DO.DalAlreadyExistsException exp)
            {
                throw new Exceptions.BOAlreadyExistsException(exp.Message);//לשנות לסוג אקספשיון הנדרש
            }
           
        }
        else
            throw new Exception("inncorrect input");//לשנות לסוג אקספשיון הנדרש

    }

    public void Delete(int id)
    {
        bool exist = false;
        
            IEnumerable<DO.Order?> orders = new List<DO.Order?>(Dal.Order.GetAll());
        foreach (var ord in orders)
        {
            IEnumerable<DO.OrderItem?> ordi = new List<DO.OrderItem?>();
            ordi=Dal.OrderItem.GetByOrderId(ord?.ID??throw new Exception("order id is missing"));
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
        
    

    public IEnumerable<ProductForList?> GetProducts()
    {
        IEnumerable<DO.Product?> pro=Dal.Product.GetAll();

        return pro.Select(pr => new BO.ProductForList
        {
            ID = pr?.ID??throw new Exception("missing id"),
            Name = pr?.Name??throw new Exception("missing name"),
            Price = pr?.Price??throw new Exception("missing price"),
            Category = (BO.Category)pr.Category,
        });
        
       
        
    }

    public BO.Product? ItemProduct(int id)
    {
        if(id>0)
        {
            DO.Product? dopro;
            try
            {
                dopro = Dal.Product.GetById(id);
            }
            catch(DO.DalDoesNotExistException exp)
            {
                throw new Exception(exp.Message); //לשנות לסוג אקספשיון הנדרש
            }
            BO.Product? bopro = new()
            {
                ID=dopro?.ID??throw new Exception("ID is missing"),  //לשנות לסוג אקספשיון הנדרש
                Name =dopro?.Name??throw new Exception("Name is missing"),  //לשנות לסוג אקספשיון הנדרש
                Price =dopro?.Price??0,
                Category=(BO.Category?)dopro?.Category??throw new Exception("category is missing"),
                InStock=dopro?.InStock??0,//??
            };
            return bopro;
        }
        return null;    
    }

    public BO.ProductItem? ItemProduct(int id, BO.Cart cart)
    {
        if(id>0)
        {
            DO.Product p;
            try
            {
              p = Dal.Product.GetById(id);
            }
            catch(DO.DalDoesNotExistException exp)
            {
                throw new Exceptions.BODoesNotExistException(exp.Message);
            }
            BO.ProductItem? proi = new()
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                isStock = p.InStock > 0 ? true : false,
                AmountCart = 0,//לשנות!
            };
            return proi;    
          
        }
        return null;
    }

    public void Update(BO.Product p)
    {
        if (p.ID > 0 && p.Name != "" && p.Name != null && p.Price > 0 && p.InStock > 0)
        {
            DO.Product dp = new()
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                InStock = p.InStock,
                Category = (DO.Category)p.Category
            };
            try
            {
                Dal.Product.Update(dp);
            }
            catch (DO.DalDoesNotExistException exp)
            {
                throw new Exceptions.BODoesNotExistException(exp.Message);
            }

        }
        else
            throw new Exception("inncorrect input");
    }
}
