﻿namespace NRM.Models.DataModels
{
    public class ParcelType
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

        public List<Parcel>? Parcels { get; set; }
    }
}
