﻿namespace ShopFloorPlacementPlanner
{
    internal class connectionStrings
    {
        public const string ConnectionString = "user id=sa;" +
                     "password=Dodid1;Network Address=192.168.0.150\\sqlexpress;" +
                     "Trusted_Connection=no;" +
                     "database=order_database; " +
                     "connection timeout=30";

        public const string ConnectionStringUser = "user id=sa;" +
                               "password=Dodid1;Network Address=192.168.0.150\\sqlexpress;" +
                               "Trusted_Connection=no;" +
                               "database=user_info; " +
                               "connection timeout=30";
    }
}