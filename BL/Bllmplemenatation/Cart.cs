﻿namespace BlImplementation;
using BlApi;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


/// <summary>
/// Implementation of CART
/// </summary>
internal class Cart : ICart
{
    private static readonly Random _rnd = new();
    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;

    /// <summary>
    /// Updating the quantity of a product in the shopping cart (for the shopping cart screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.Exceptions.BlInvalidInputException"></exception>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]    
    public BO.Cart Update(BO.Cart cart, int id, int amount)
    {
        BO.OrderItem? boOrdi = cart.Items?.FirstOrDefault(x => x?.ProductID == id);
        if (boOrdi == null)
            throw new BO.BlMissingEntityException("prodcut  dose not exists in cart");
        if (amount < 0)
            throw new BO.BlInCorrectException("invlavel amount");
        if (amount == 0)
        {
            cart.Items?.Remove(boOrdi);
            cart.TotalPrice -= boOrdi.Totalprice;
        }
        else
        {
            DO.Product? p;
            try
            {
                lock (dal)
                {
                    p = dal.Product.GetById(id);
                }
            }
              catch (DO.UnFoundException exp)
            {
                throw new BO.BlMissingEntityException("the product does not exist", exp);
            }
            if(p?.InStock>=amount)
            {
                if (amount < boOrdi.Amount)
                {
                    cart.Items?.Remove(boOrdi);
                    cart.TotalPrice -= (boOrdi.Amount - amount) * boOrdi.Price;
                    boOrdi.Amount = amount;
                    boOrdi.Totalprice = boOrdi.Price * amount;
                    cart.Items?.Add(boOrdi);
                }
                if (amount > boOrdi.Amount)
                {
                    cart.Items?.Remove(boOrdi);
                    cart.TotalPrice += (amount - boOrdi.Amount) * boOrdi.Price;
                    boOrdi.Amount = amount;
                    boOrdi.Totalprice = boOrdi.Price * amount;
                    cart.Items?.Add(boOrdi);
                }

            }
            else
            {
                throw new BO.BlMissingEntityException("therse is not enough from the product in stock");
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
    [MethodImpl(MethodImplOptions.Synchronized)]    
    public BO.Cart Add(BO.Cart cart, int id)
    {
        if (id <= 0)
            throw new BO.BlInCorrectException("wrong negative id");
        DO.Product? pro;
        try
        {
            lock (dal)
            {
                pro = dal?.Product.GetById(delegate (DO.Product? x) { return x?.ID == id; });
            }
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException("the product does not exist", exp);
        }
        BO.OrderItem? boOrderItem = cart.Items?.FirstOrDefault(x => x?.ProductID == id);
        if (pro?.InStock > 0)
        {
            if (boOrderItem == null)// OrderItem does not exist
            {
                cart.Items?.Add(new BO.OrderItem
                {
                    ProductID = pro?.ID ?? throw new BO.BlNullPropertyException("id is missing"),
                    Amount = 1,
                    Name = pro?.Name,
                    Price = pro?.Price ?? 0,
                    Totalprice = pro?.Price ?? 0,
                   
                });
                cart.TotalPrice += pro?.Price ?? throw new BO.BlNullPropertyException("product does not exist");
            }
            else // OrderItem exists
            {
                  if (pro?.InStock -boOrderItem.Amount-1>= 0)
                {
                    cart.Items?.Remove(boOrderItem);
                    boOrderItem.Amount++;
                    boOrderItem.Totalprice += pro?.Price ?? 0;
                    cart.Items?.Add(boOrderItem);
                    cart.TotalPrice += pro?.Price ?? 0;
                }
                  else
                    throw new BO.BlMissingEntityException("Product not in stock for that amaunt-cant add no more");

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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int MakeAnOrder(BO.Cart cart)
    {
        //All the products exist (according to the ID card, although it is possible that they exist with zero quantity
        try
        {
            lock (dal)
            {
                cart.Items?.ForEach(x => dal?.Product.GetById(x!.ProductID));
            }
        }
        catch (DO.UnFoundException exp)
        {
            throw new BO.BlMissingEntityException("one of the products in the cart does not exsist", exp);
        }
        if (cart.Items?.Exists(x => x?.Amount <= 0) ?? throw new BO.BlNullPropertyException("Items does not exist"))
            throw new BO.BlInCorrectException("negetive amount");
        if (cart.Items.Exists(x => x?.Amount > dal?.Product.GetById(x?.ProductID ?? throw new BO.BlNullPropertyException("OrderItems does not exist"))?.InStock))
            throw new BO.BlInCorrectException("therse not enogth instook");
        if (cart.CustomerName == null)
            throw new BO.BlNullPropertyException("missing name");
        if (cart.CustomerAdress == null)
            throw new BO.BlNullPropertyException("missing adress");
        if (cart.CustomerEmail == null)
            throw new BO.BlNullPropertyException("missing Email");
        if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
            throw new BO.BlInCorrectException("invlavel email");
        int days = _rnd.Next(21, 200);
        int orderId;
        DO.Order ord = new()
        {
            CustomerAdress = cart.CustomerAdress,
            CustomerEmail = cart.CustomerEmail,
            CustomerName = cart.CustomerName,
            ID = 1,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryrDate = null,
            Amount = cart.Items.Select(x => x?.Amount ?? throw new BO.BlNullPropertyException("OrderItem does not exist")).Sum(),
        };
        lock (dal)
        {
            orderId = dal!.Order.Add(ord); // There is no need to check that there is no exception for adding an order
        }
        if (cart.Items.Count <= 0)
            throw new BO.BlInCorrectException("cant make an order because the cart is empty");
        foreach (var bordi in cart.Items)
        {
            DO.OrderItem dOrdi = new()
            {
                ID = bordi?.ID ?? throw new BO.BlNullPropertyException("OrderItem does not exist"), // negligible
                ProductID = bordi.ProductID,
                OrderID = orderId,
                Price = bordi.Price,
                Amount = bordi.Amount,
            };
            dal?.OrderItem.Add(dOrdi); // There can be no exception
            DO.Product? p;
            try
            {
                lock (dal)
                {
                    p = dal?.Product.GetById(dOrdi.ProductID);
                }
            }
            catch (DO.UnFoundException exp)
            {
                throw new BO.BlMissingEntityException("The priduct does not exsist", exp);
            }

            if (p is DO.Product product)
            {
                product.InStock -= dOrdi.Amount;
                try
                {
                    dal?.Product.Update(product);
                }
                catch (DO.UnFoundException exp)
                {
                    throw new BO.BlMissingEntityException("the priduct does not exsist", exp);
                }
               
            }
        }
        cart.Items.Clear();
        cart.TotalPrice = 0;
        return orderId;
    }
}

