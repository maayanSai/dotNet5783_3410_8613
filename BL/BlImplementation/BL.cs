﻿using BlApi;



namespace BlImplementation;

internal class BL:IBL
{
    DalApi.IDal Dal = new Dal.DalList();
}