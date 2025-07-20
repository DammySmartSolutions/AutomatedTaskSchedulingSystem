






























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
        url: "../api/ATSS/LoadSystemUser",
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
                "columnDefs": [{ "width": "30%", "targets": 0 }, { "width": "40%", "targets": 1 }, { "width": "10%", "targets": 2 }, { "width": "10%", "targets": 3 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

                    'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'EmpID' },
                    { 'data': 'Email' },



                    {


                        'render': function (data, type, full, row) {

                            return '<a id=btnEdit class="btn  btn-dark"> <i class="fa fa-fw fas fas fa-edit" > Edit</i> </a>'



                        }

                    },

                    {

                        'render': function (data, type, full, row) {

                            return '<a id=btnDelete class="btn  btn-danger mx-2"> <i class="fa fa-fw fas  fas fa-times" > Delete</i>   </a>'



                        }


                    },



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
                id = Data["SysUserID"]





                document.getElementById("MainContent_txtempid").value = Data["EmpID"];
                document.getElementById("MainContent_txtemail").value = Data["Email"];


                document.getElementById("MainContent_ComID").value = id;


                //Mainform.ComID.value = id;



                //$("#MainContent_ddlLoc option").each(function () {
                //    if ($(this).val() == Data["LocID"]) {
                //        $(this).prop("selected", true);
                //    }
                //});














            });

            $('#table tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["SysUserID"]
                var EmpID = Data["EmpID"];



                var Result = confirm("You are about to delete System User with EmployeeID: " + EmpID)
                if (Result) {

                    DeleteItem(ID, EmpID);
                }

            });


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







function DeleteItem(ID, EmpID) {


    //var CheckCookieValue = document.cookie.split("=");
    //var getcomid = CheckCookieValue[2];
    $.ajax({
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/ATSS/DeleteSystemUser?id=" + ID + "&empid=" + EmpID,

        success: function (data) {
            //  alert("Task Deleted!");

            document.getElementById("lblmsg").innerText = data;
            ID = 0;
            LoadDataTable();



        }
    })

}



