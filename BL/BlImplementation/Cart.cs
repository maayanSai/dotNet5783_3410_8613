

namespace BlImplementation;
using BlApi;
using BO;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

internal class Cart:ICart
{
    private static readonly Random _rnd = new();
    DalApi.IDal Dal = new Dal.DalList();
    public BO.Cart? Update(BO.Cart cart, int id, int amount)
    {
        BO.OrderItem? boOrdi = cart?.Items?.FirstOrDefault(x => x.ProductID == id);
        if (boOrdi == null)
            throw new Exceptions.BlInvalidInputException("prodcut  dose not exists in cart");

        if( amount<0)
            throw new Exception("invlavel amaunt");
        if (amount == 0)
            cart?.Items?.Remove(boOrdi);
        if(amount <boOrdi .Amount )
        {
            cart?.Items?.Remove(boOrdi);
            cart!.TotalPrice-=(boOrdi.Amount-amount)*boOrdi.Price;
            boOrdi.Amount = amount;
            boOrdi.Totalprice=boOrdi.Price*amount;
            cart?.Items?.Add(boOrdi); 
        }
        if (amount >boOrdi.Amount)
        {
            cart?.Items?.Remove(boOrdi);
            cart!.TotalPrice+=(amount-boOrdi.Amount)*boOrdi.Price;  
            boOrdi.Amount = amount;
            boOrdi.Totalprice=boOrdi.Price*amount;
            cart?.Items?.Add(boOrdi);

        }
        return cart;




    }
    public BO.Cart? Add(BO.Cart cart, int id)
    {
        if (id <= 0)
            throw new Exception("wrong id");
        DO.Product pro;
        try
        {
            pro = Dal.Product.GetById(id);
        }
        catch (DO.DalDoesNotExistException exp)
        {
            throw new Exception(exp.Message);  //!!! 
        }
        var boOrderItem = cart?.Items?.FirstOrDefault(x => x.ProductID == id);
        if (pro.InStock > 0)
        {
            if (boOrderItem == null)//האורדר אייט לא קיים
            {
                cart?.Items?.Add(new BO.OrderItem
                {
                    ProductID = pro.ID,
                    Amount = 1,
                    Name = pro.Name,
                    Price = pro.Price,
                    Totalprice = pro.Price
                    //ID??
                });
                cart!.TotalPrice = (cart?.TotalPrice ?? 0) + pro.Price;//?
            }

            else//האורדראייטם קיים
            {
                cart?.Items?.Remove(boOrderItem);
                boOrderItem.Amount++;
                boOrderItem.Totalprice += pro.Price;
                cart?.Items?.Add(boOrderItem);
                cart!.TotalPrice += pro.Price;

            }
        }
        else
            throw new Exception("not in stock");
       

            

        return cart;
    }

    public void MakeAnOrder(BO.Cart cart)
    {
        //כל המוצרים קיימים(לפי התעודת זהות,למרות שיכול להיות שהם קיימים עם כמות אפס
        try
        {
            cart?.Items?.ForEach(x => Dal.Product.GetById(x.ProductID));
        }
        catch(DO.DalDoesNotExistException exp)
        {
            throw new Exceptions.BODoesNotExistException(exp.Message);   
        }
        if (cart!.Items!.Exists(x => x.Amount<=0))
            throw new Exceptions.BlInvalidInputException("negetive amount");
        if (cart.Items.Exists(x => x.Amount>Dal.Product.GetById(x.ProductID).InStock))
            throw new Exception("therse not enogth instook");
        if (cart.CustomerName==null)
            throw new Exceptions.BlInvalidInputException("missing name");
        if(cart.CustomerAdress==null)
            throw new Exceptions.BlInvalidInputException("missing adress");
        if(cart .CustomerEmail==null)
            throw new Exceptions.BlInvalidInputException("missing Email");
        if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
            throw new Exceptions.BlInvalidInputException("invlavel email");
        int days = _rnd.Next(21, 200);
        int orderId;
        DO.Order ord = new()
        {
            CustomerAdress = cart.CustomerAdress,
            CustomerEmail=cart.CustomerEmail,
            CustomerName=cart.CustomerName,
            ID=1,
            OrderDate=DateTime.Now,
            ShipDate=null,
            DeliveryrDate=null,
            Amount=cart.Items.Select(x => x.Amount).Sum(),
        };


        orderId=Dal.Order.Add(ord);//לא צריך לבדוק כי אין חריגה להוספה של אורדר
        foreach(var bordi in cart.Items)
        {
            DO.OrderItem dOrdi = new()
            {
                ID=bordi.ID,//זניח
                ProductID=bordi.ProductID,
                OrderID=orderId,
                Price=bordi.Price,
                Amount=bordi.Amount,
            };
            Dal.OrderItem.Add(dOrdi); //לא יכולה להיות חריגה
            DO.Product p;
            try
            {
                 p= Dal.Product.GetById(dOrdi.ID);
            }
            catch (DO.DalDoesNotExistException exp)
            {
                throw new Exceptions.BODoesNotExistException(exp.Message);
            }
            p.InStock-=dOrdi.Amount;
            try 
            {
                Dal.Product.Update(p);
            }
            catch (DO.DalDoesNotExistException exp)
            {
                throw new Exceptions.BODoesNotExistException(exp.Message);
            }

        }
       



    }


}

