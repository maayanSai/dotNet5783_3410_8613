﻿namespace BlImplementation;
using BlApi;
using BO;
using System.ComponentModel.DataAnnotations;

internal class Cart:ICart
{
    private static readonly Random _rnd = new();
    DalApi.IDal Dal = new Dal.DalList();
    /// <summary>
    /// Updating the quantity of a product in the shopping cart (for the shopping cart screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.Exceptions.BlInvalidInputException"></exception>
    /// <exception cref="Exception"></exception>
    public BO.Cart? Update(BO.Cart cart, int id, int amount)
    {
        OrderItem ?boOrdi = cart.Items?.FirstOrDefault(x => x.ProductID == id);
        if (boOrdi == null)
            throw new BO.BlMissingEntityException("prodcut  dose not exists in cart");

        if( amount<0)
            throw new BO.BlInCorrectException("invlavel amount");
        if (amount == 0)
        {
            cart.Items?.Remove(boOrdi);
            cart.TotalPrice -= boOrdi.Totalprice;
        }
        else
        {
            if (amount < boOrdi.Amount)
            {
                cart?.Items?.Remove(boOrdi);
                cart!.TotalPrice -= (boOrdi.Amount - amount) * boOrdi.Price;
                boOrdi.Amount = amount;
                boOrdi.Totalprice = boOrdi.Price * amount;
                cart?.Items?.Add(boOrdi);
            }
            if (amount > boOrdi.Amount)
            {
                cart?.Items?.Remove(boOrdi);
                cart!.TotalPrice += (amount - boOrdi.Amount) * boOrdi.Price;
                boOrdi.Amount = amount;
                boOrdi.Totalprice = boOrdi.Price * amount;
                cart?.Items?.Add(boOrdi);
            }
        }
        return cart;
    }
    /// <summary>
    /// Adding a product to the shopping cart (for catalog screen and product details screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public BO.Cart Add(BO.Cart cart, int id)
    {
        if (id <= 0)
            throw new BO.BlInCorrectException("wrong negative id");
        DO.Product pro;
        try
        { 
            pro = Dal.Product.GetById(id);
        }
        catch (DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException("the product does not exist",exp);   
        }
        BO.OrderItem? boOrderItem = cart.Items?.FirstOrDefault(x => x.ProductID == id);
        if (pro.InStock > 0)
        {
            if (boOrderItem == null)//האורדר אייט לא קיים
            {
                cart.Items?.Add(new BO.OrderItem
                {
                    ProductID = pro.ID,
                    Amount = 1,
                    Name = pro.Name,
                    Price = pro.Price,
                    Totalprice = pro.Price
                });
                cart!.TotalPrice +=pro.Price;
            }
            else//האורדראייטם קיים
            {
                cart.Items?.Remove(boOrderItem);
                boOrderItem.Amount++;
                boOrderItem.Totalprice += pro.Price;
                cart.Items?.Add(boOrderItem);
                cart.TotalPrice += pro.Price;
            }
        }
        else
            throw new BO.BlMissingEntityException("Product not in stock");
        return cart;
    }
    /// <summary>
    /// Confirmation for the order basket (for the shopping basket screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <exception cref="Exceptions.BODoesNotExistException"></exception>
    /// <exception cref="Exceptions.BlInvalidInputException"></exception>
    /// <exception cref="Exception"></exception>
    public void MakeAnOrder(BO.Cart cart)
    {
        //כל המוצרים קיימים(לפי התעודת זהות,למרות שיכול להיות שהם קיימים עם כמות אפס
        try
        {
            cart.Items?.Exists(x=> Dal.Product.GetById(x.ProductID).ID==x.ProductID);
        }
        catch(DO.DalMissingIdException exp)
        {
            throw new BO.BlMissingEntityException("one of the products in the cart does not exsist",exp);   
        }
        if (cart!.Items!.Exists(x => x.Amount<=0))
            throw new BO.BlInCorrectException("negetive amount");
        if (cart.Items.Exists(x => x.Amount>Dal.Product.GetById(x.ProductID).InStock))
            throw new BO.BlInCorrectException("therse not enogth instook");
        if (cart.CustomerName==null)
            throw new BO.BlNullPropertyException("missing name");
        if(cart.CustomerAdress==null)
            throw new BO.BlNullPropertyException("missing adress");
        if(cart .CustomerEmail==null)
            throw new BO.BlNullPropertyException("missing Email");
        if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
            throw new BO.BlInCorrectException("invlavel email");
        int days = _rnd.Next(21, 200);
        int orderId;
        //
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
                 p= Dal.Product.GetById(dOrdi.ProductID);
            }
            catch (DO.DalMissingIdException exp)
            {
                throw new BO.BlMissingEntityException("The priduct does not exsist",exp);
            }
            p.InStock-=dOrdi.Amount;
            try 
            {
                Dal.Product.Update(p);
            }
            catch (DO.DalMissingIdException exp)
            {
                throw new BO.BlMissingEntityException("the priduct does not exsist",exp);
            }

        }


        //cart.Items.Clear();
        //cart.TotalPrice=0;

    }
 


}

