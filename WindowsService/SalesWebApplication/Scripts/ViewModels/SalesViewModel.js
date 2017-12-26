var SalesViewModel = function ()
{
    var self = this;

    this.Sales = ko.observableArray([]);
    this.Product = ko.observableArray([]);

    //this.DateTime = ko.observable().extend({ required: true });
    //this.ManagerName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    //this.CustomerName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    //this.ProductName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    //this.Amount = ko.observable().extend({ required: true });

    this.DateTime = '';
    this.ManagerName = '';
    this.CustomerName = '';
    this.ProductName = '';
    this.Amount = '';

    this.IsActive = ko.observable(false);

    this.ok = function ()
    {
        self.Sales.push({
            DateTime: self.DateTime,
            ManagerName: self.ManagerName,
            CustomerName: self.CustomerName,
            ProductName: self.ProductName,
            Amount: self.Amount
        });

        $.ajax({
            type: 'POST',
            url: '/Home/AddSales',
            data: ko.toJSON(self),
            success: function (data) {
                self.IsActive(false);
            },
            contentType: "application/json",
            dataType: 'json'
        });
        self.cancel();  
    }

    this.cancel = function ()
    {
        self.IsActive(false);

        $('#DateTime').val('');
        $('#ManagerName').val('');
        $('#CustomerName').val('');
        $('#ProductName').val('');
        $('#Amount').val('');
    }

    this.showAddSalesForm = function ()
    {
        self.IsActive(!self.IsActive());
    }

    var loadSales = function ()
    {
        $.ajax({
            url: '/Home/GetSales',
            type: "GET",
            success: function (data)
            {
                self.Sales(data);
            }
        });
    }
    loadSales();

    this.deleteSales = function (Id)
    {
        $.ajax({
            type: 'POST',
            url: '/Home/DeleteByIdSales',
            data: ko.toJSON(Id),
            success: function (data) {

            },
            contentType: "application/json",
            dataType: 'json'
        });
        self.Sales.remove(this);
    }
};
