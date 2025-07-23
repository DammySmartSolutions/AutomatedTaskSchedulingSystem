


























//id = 0;
ID = 0;
const Mainform = document.getElementById("mainform")
window.onload = function onloadTable() {

    LoadDataTable();
}





function LoadDataTable() {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/ATSS/LoadEmployeeList",
        success: function (data) {


            $('#table').DataTable().destroy();

            var datatableVariable = $('#table').DataTable({


                data: data,
                "bFilter": true,
                "bInfo": false,
                "searching": true,
                "ordering": true,


                "bDisplayLength": 1000,
                "bLengthChange": true,
                "lengthMenu": [[5, 10, 15, 20, 25, 50, -1], [5, 10, 25, 50, "All"]],
                "columnDefs": [{ "width": "20%", "targets": 0 }, { "width": "40%", "targets": 1 }, { "width": "10%", "targets": 2 }, { "width": "30%", "targets": 3 }, { "width": "10%", "targets": 4 } ],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

                    'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'EmpID' },

                    { 'data': 'FullName' },

                    { 'data': 'Sex' },

                    { 'data': 'Position' },



                    {


                        'render': function (data, type, full, row) {

                            return '<a id=btnEdit class="btn  btn-primary" style="color:white"> <i class="fa fa-fw fas fas fa-edit" > Edit</i> </a>'



                        }

                    },

                    //{

                    //    'render': function (data, type, full, row) {

                    //        return '<a id=btnDelete class="btn  btn-danger mx-2"> <i class="fa fa-fw fas  fas fa-times" > Delete</i>   </a>'



                    //    }


                    //},



                ]



            });


            datatableVariable.buttons(0, null).container().appendTo(datatableVariable.table().container());
            $('#table tfoot th').each(function () {
                var placeHolderTitle = $('#table thead th').eq($(this).index()).text();
                $(this).html('<input type="text" class="form-control input input-sm" placeholder = "Search ' + placeHolderTitle + '" />');
            });
            datatableVariable.columns().every(function () {
                var column = this;
                $(this.footer()).find('input').on('keyup change', function () {
                    column.search(this.value).draw();
                });
            });
            $('.showHide').on('click', function () {
                var tableColumn = datatableVariable.column($(this).attr('data-columnindex'));
                tableColumn.visible(!tableColumn.visible());
            });








            $('#table tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();


                id = Data["ID"];
               var Empid = Data["EmpID"]


                             
                document.getElementById("MainContent_userid").value = id;




                window.location.href = "AddEmployee.aspx?Empid=" + Empid + "";



                












            });

            //$('#table tbody').on('click', "#btnDelete", function () {

            //    var currentrow = $(this).closest("tr")
            //    var table = $('#table').DataTable()
            //    var Data = table.row(currentrow).data();
            //    ID = Data["LocID"]
            //    var Loc = Data["Location"];



            //    var Result = confirm("You are about to delete " + Loc + " Location")
            //    if (Result) {

            //        DeleteItem(ID);
            //    }

            //});


            // if it is a datatable


        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
            alert(thrownError);
        }

    });

}







function DeleteItem(ID) {


    //var CheckCookieValue = document.cookie.split("=");
    //var getcomid = CheckCookieValue[2];
    $.ajax({
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/ATSS/DeleteLocation?id= " + ID + "",
        success: function (data) {
            alert("Location Deleted!");
            ID = 0;
            LoadDataTable();



        }
    })

}



