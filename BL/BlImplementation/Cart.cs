

namespace BlImplementation;
using BlApi;
using BO;
using System.Transactions;

internal class Cart:ICart
    {
    DalApi.IDal Dal = new Dal.DalList();
    public BO.Cart? Update(BO.Cart cart, int id, int amount)
    {
        BO.OrderItem boOrdi = cart.Items.FirstOrDefault(x => x.ProductID == id);
        if (boOrdi == null)
            throw new Exceptions.BlInvalidInputException("prodcut  dose not exists in cart");

        if( amount<0)
            throw new Exception("invlavel amaunt");
        if (amount == 0)
            cart.Items.Remove(boOrdi);
        if(amount <boOrdi .Amount )




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
                cart.TotalPrice = (cart?.TotalPrice ?? 0) + pro.Price;//?
            }

            else//האורדראייטם קיים
            {
                cart.Items.Remove(boOrderItem);
                boOrderItem.Amount++;
                boOrderItem.Totalprice += pro.Price;
                cart.Items.Add(boOrderItem);
                cart.TotalPrice += pro.Price;

            }
        }
        else
            throw new Exception("not in stock");
       

            

        return cart;
    }

    public void MakeAnOrder(BO.Cart cart)
    {
        throw new NotImplementedException();
    }

 
}

