namespace CosmeticsStore.Data
{
   public class DataConstants
    {
        public class Product
        {
            public const int BrandMaxLength = 50;
            public const int NameMaxLength = 80;
            public const int DescriptionMaxLength = 300;
            public const int MinLength = 2;
            public const int QuantityMin = 1;
            public const int QuantityMax = 9999;
        }

        public class Category
        {
            public const int NameMaxLength = 50;
        }

        public class Dealer
        {
            public const int NameMaxLenght = 40;
            public const int NameMinLength = 2;
            public const int PhoneMinLength = 6;
            public const int PhoneMaxLength = 20;
        }
        public class User
        {
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 45;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }

        public class Delivery
        {
            public const int AddressMaxLenght = 200;
            public const int PhoneMinLength = 6;
            public const int PhoneMaxLength = 20;
            public const int RecipientMinLength = 8;
            public const int RecipientMaxLength = 100;
        }
    }
}
