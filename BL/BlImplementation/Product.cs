

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
                Name=p.Name??throw new Exception ("wrong name"),//לשנות לסוג אקספשיון הנדרש
                Price=p.Price,
                Category=(DO.Category)p.Category,//צריך לבדוק אם הומר תקין?
                InStock=p.InStock,
            };
            try
            {
                Dal.Product.Add(p1);
            }
            catch (DO.DalAlreadyExistsException exp)
            {
                throw new Exception(exp.Message);//לשנות לסוג אקספשיון הנדרש
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
                throw new Exception(exp.Message);//לשנות לסוג אקספשיון הנדרש
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
            ID=pr?.ID??throw new Exception("missing id"),
            Name=pr?.Name??throw new Exception("missing name"),
            Price=pr?.Price??0,
            Category=(BO.Category?)pr?.Category??throw new Exception("missing category")
        });
        
       
        
    }

    public BO.Product? ItemProduct(int id)
    {
        if(id>0)
        {
           DO.Product? dopro= Dal.Product.GetById(id);
            BO.Product? bopro = new() {ID= } 
        }
    }

    public BO.Product? ItemProduct(int id, BO.Cart cart)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Product p)
    {
        throw new NotImplementedException();
    }
}
