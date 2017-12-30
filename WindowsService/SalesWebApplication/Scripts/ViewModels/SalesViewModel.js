var SalesViewModel = function () {
    var self = this;

    this.Sales = ko.observableArray([]);

    //this.Product = ko.observableArray([]);

    this.Id = ko.observable();
    this.DateTime = ko.observable().extend({ required: true });
    this.ManagerName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    this.CustomerName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    this.ProductName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    this.Amount = ko.observable().extend({ required: true });

    this.errors = ko.validation.group(self);

    var loadSales = function () {
        $.ajax({
            url: '/Home/GetSales',
            type: "GET",
            success: function (data) {
                self.Sales(data);
            }
        });
    }
    loadSales();


    this.save = function ()
    {
        var id = $('#Id').val();

        if (id == 0) {
            self.addSales();
        }
        else
        {
            self.editSales();
        }
    }

    this.addSales = function ()
    {
        if (self.errors().length === 0)
        {
            var obj = function ()
            {
                this.DateTime = self.DateTime;
                this.ManagerName = self.ManagerName;
                this.CustomerName = self.CustomerName;
                this.ProductName = self.ProductName;
                this.Amount = self.Amount;
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
                    self.clearForm();
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

    this.loadSalesForm = function () {
        $('#NameForm').text('Добавление записи');
    }

    this.loadSalesEditForm = function (context)
    {
        $('#NameForm').text('Редактирование записи');
        var id = context.Id;
        var objSales = ko.utils.arrayFirst(self.Sales(), function (item){
            return item.Id === id;
        });
        var date = moment(objSales.DateTime);

        self.Id(id);
        self.DateTime(date);
        self.ManagerName(objSales.ManagerName);
        self.CustomerName(objSales.CustomerName);
        self.ProductName(objSales.ProductName);
        self.Amount(objSales.Amount);
    }

    this.editSales = function () {
        $.ajax({
            type: 'POST',
            url: '/Home/EditSales',
            data: ko.toJSON(self),
            success: function (data) {

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
        //$('#Id').val('');
        //$('#DateTime').val('');
        //$('#ManagerName').val('');
        //$('#CustomerName').val('');
        //$('#ProductName').val('');
        //$('#Amount').val('');

        self.Id('');
        self.DateTime('');
        self.ManagerName('');
        self.CustomerName('');
        self.ProductName('');
        self.Amount('');

        $('#myModal').modal('hide');

    }
};
