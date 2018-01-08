var CustomerViewModel = function ()
{
    var self = this;

    this.Id = ko.observable();
    this.CustomerName = ko.observable().extend({ required: true, minLength: 2, maxLength: 30 });;
    this.Customers = ko.observableArray([]);

    this.errors = ko.validation.group(self);

    var getCustomers = function () {
        $.ajax({
            type: "Post",
            url: '/Customer/GetCustomers',
            success: function (result) {
                self.Customers(result);
            }
        });
    }
    getCustomers();

    this.addCustomer = function ()
    {
        if (self.errors().length === 0)
        {
            $.ajax({
                type: "Post",
                url: '/Customer/AddCustomer',
                data: ko.toJSON(self),
                success: function(id) {
                    self.Customers.unshift(
                        {
                            Id: id,
                            FullName: self.CustomerName()
                        });
                    self.clearForm();
                },
                contentType: "application/json",
                dataType: 'json'
            });
        }
    }

    this.loadCustomerEditForm = function (context) {
        var id = context.Id;
        var objCustomer = ko.utils.arrayFirst(self.Customers(),
            function (item) {
                return item.Id === id;
            });
        self.Id(objCustomer.Id);
        self.CustomerName(objCustomer.FullName);
    }

    this.editCustomer = function (content)
    {
        if (self.errors().length === 0)
        {
            var Obj = function() {
                this.Id = self.Id();
                this.FullName = self.CustomerName();
            }

            $.ajax({
                type: 'POST',
                url: '/Customer/EditCustomer',
                data: ko.toJSON(new Obj),
                success: function() {

                },
                contentType: "application/json",
                dataType: 'json'
            });

            var objCustomer = ko.utils.arrayFirst(self.Customers(),
                function(item) {
                    return item.Id === self.Id();
                });
            self.Customers.replace(objCustomer, new Obj);

            self.clearForm();
        }
    }

    this.deleteCustomer = function (content) {
        self.Id(content.Id);

        $.ajax({
            type: "Post",
            url: '/Customer/DeleteCustomer',
            data: ko.toJSON(self),
            success: function () {

            },
            contentType: "application/json",
            dataType: 'json'
        });
        self.Customers.remove(this);
    }

    this.clearForm = function () {
        self.Id(null);
        self.CustomerName(null);
        $('#AddFormModal').modal('hide');
        $('#EditFormModal').modal('hide');
    }
}