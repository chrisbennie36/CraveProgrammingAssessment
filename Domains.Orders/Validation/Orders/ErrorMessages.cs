namespace Domains.Ordering.Validation.Orders
{ 
    public static class ErrorMessages
    {
        public const string NoCustomerDataSupplied = "Customer data must be supplied with an Order";
        public const string NoCustomerNameSupplied = "Customer name must be supplied with an Order";
        public const string OrderedProductDoesNotExist = "Ordered Product does not exist";
        public const string OrdredProductIsNotAvailable = "Ordered Product is not currently available";
        public const string OrderedServiceMethodDoesNotExist = "Ordered Service Method does not exist";
    }
}
