var SalesViewModel = function ()
{
    var self = this;

    this.Id = ko.observable();
    this.DateTime = ko.observable().extend({ required: true });
    this.ManagerName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    this.CustomerName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    this.ProductName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    this.Amount = ko.observable().extend({ required: true });

    this.ManagersList = ko.observableArray([]);
    this.CustomersList = ko.observableArray([]);
    this.ProductsList = ko.observableArray([]);
    this.Manager = ko.observable();
    this.Customer = ko.observable();
    this.Product = ko.observable();

    this.Sales = ko.observableArray([]);

    this.errors = ko.validation.group(self);

    var loadSales = function ()
    {
        $.ajax({
            url: '/Home/GetSales',
            type: "GET",
            success: function (data) {
                self.Sales(data);
            }
        });
    }
    loadSales();


    //this.save = function ()
    //{
    //    var id = $('#Id').val();

    //    if (id == 0)
    //    {
    //        self.addSales();
    //    }
    //    else
    //    {
    //        self.editSales();
    //    }
    //}

    this.addSales = function ()
    {
        if (self.errors().length === 0)
        {
            var obj = function ()
            {
                this.DateTime = self.DateTime();
                this.ManagerName = self.ManagerName();
                this.CustomerName = self.CustomerName();
                this.ProductName = self.ProductName();
                this.Amount = self.Amount();
            };

            $.ajax({
                type: 'POST',
                url: '/Home/AddSales',
                data: ko.toJSON(new obj()),
                success: function (id)
                {
                    var date = moment(self.DateTime());
                    self.Sales.push(
                    {
                        Id: id,
                        DateTime: date.format('DD.MM.YYYY hh:mm:ss'),
                        ManagerName: self.ManagerName(),
                        CustomerName: self.CustomerName(),
                        ProductName: self.ProductName(),
                        Amount: self.Amount()
                    });
                    //self.clearForm();
                },
                contentType: "application/json",
                dataType: 'json'
            });
        }
        else
        {
            self.errors.showAllMessages();
        }
    }


    this.loadSalesEditForm = function (context)
    {

        var id = context.Id;
        var objSales = ko.utils.arrayFirst(self.Sales(), function (item){
            return item.Id === id;
        });

        var date = moment(objSales.DateTime,'DD-MM-YYYY hh:mm:ss');

        self.Id(id);
        self.DateTime(date.format('YYYY-MM-DDTHH:mm:ss'));
        self.Amount(objSales.Amount);

        $.ajax({
            url: '/Home/GetAllManagers',
            type: "GET",
            success: function (managersList)
            {
                self.ManagersList(managersList);
                self.Manager(objSales.ManagerName);
            }
        });

        $.ajax({
            url: '/Home/GetAllCustomers',
            type: "GET",
            success: function (customersList)
            {
                self.CustomersList(customersList);
                self.Customer(objSales.CustomerName);
            }
        });

        $.ajax({
            url: '/Home/GetAllProducts',
            type: "GET",
            success: function (productsList)
            {
                self.ProductsList(productsList);
                self.Product(objSales.ProductName);
            }
        });
    }

    this.editSales = function ()
    {
        var obj = function () {
            this.Id = self.Id();
            this.DateTime = self.DateTime();
            this.ManagerName = self.ManagerName();
            this.CustomerName = self.CustomerName();
            this.ProductName = self.ProductName();
            this.Amount = self.Amount();
        };

        $.ajax({
            type: 'POST',
            url: '/Home/EditSales',
            data: ko.toJSON(new obj()),
            success: function () {
            },
            contentType: "application/json",
            dataType: 'json'
        });
    }

    this.deleteSales = function (id) {
        $.ajax({
            type: 'POST',
            url: '/Home/DeleteByIdSales',
            data: ko.toJSON(id),
            success: function () {
            },
            contentType: "application/json",
            dataType: 'json'
        });
        self.Sales.remove(this);
    }

    this.clearForm = function ()
    {
        self.Id(null);
        self.DateTime(null);
        self.ManagerName(null);
        self.CustomerName(null);
        self.ProductName(null);
        self.Amount(null);

        this.ManagersList(null);
        this.CustomersList(null);
        this.ProductsList(null);

        //$('#myModal').modal('hide');
    }

};
