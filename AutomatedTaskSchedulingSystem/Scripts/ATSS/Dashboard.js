





    window.onload = function onloadTable() {


    LoadTotalNumberofEmployee();
    LoadTotalSysUsers();
        LoadTotalLocation();
        LoadTotalTask();
    LoadEmployeeAvail();
   
       
       

    }




function LoadTotalNumberofEmployee()
    {





        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            data: "{}",
            url: "../api/ATSS/LoadTotalEmployee",
            success: function (data) {

                document.getElementById("totalemp").innerHTML = data;

            }
        })

    }



    function LoadTotalSysUsers() {





        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            data: "{}",
            url: "../api/ATSS/LoadTotalSysUser",
            success: function (data) {

                document.getElementById("totalusers").innerHTML = data;

            }
        })

    }




    function LoadTotalLocation() {





        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            data: "{}",
            url: "../api/ATSS/LoadTotalLocation",
            success: function (data) {

                document.getElementById("totalloc").innerHTML = data;

            }
        })

    }





    function LoadTotalTask() {





        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            data: "{}",
            url: "../api/ATSS/LoadTotalTask",
            success: function (data) {

                document.getElementById("totaltask").innerHTML = data;

            }
        })

    }





function LoadEmployeeAvail() {

        var CheckCookieValue = document.cookie.split("=");
    var getcomid = CheckCookieValue[2];


    $.ajax({
        type: "GET",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    cache: false,
    data: "{ }",
        url: "../api/ATSS/LoadEmployeeAvail",
    success: function (data) {


                var elements = Array();
    elements = data;

    var datelbl = Array();
    var totlbl= Array();

    var i;

    for (i = 0; i < elements.length; i++) {
        datelbl.push(elements[i].AvailDate);
    // var getdata = elements[i].totalGrossPay;
    // var datas = getdata.replace(/,/g, '');


    //totlbl.push(parseFloat(datas));

        totlbl.push(elements[i].AvailID);
                   
                }


    var ctx = document.getElementById('barchart').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
    data: {
        labels: datelbl,
    datasets: [{
        label: 'Number of Employee Available by Date',
    data: totlbl,
    backgroundColor: [
    'blue',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'violet',
    'blue',
    'green',
    'almond',
    'maroon',
    'gold',
    'pink',
    'purple',
    '#ead'
    ],


    borderColor: [
    'black',
    'black',
    'black',
    'black',
    'black',
    'black',
    'black',
    'black',
    'black',
    'black',
    'black',
    'black',
    'black',
    'black'

    ],
    borderWidth: 1
                                    },
    ]
                                },
    options: {
        scales: {
        yAxes: [{
        display: true,
    ticks: {
        beginAtZero: true,
                                             //   min: 0,
                                              //  max: 100000000,

                                            }
                                        }]
                                    }
                                }
                
                });
                
            }

        });


    LoadEmployeeAvailByDonut();

         }






function LoadEmployeeAvailByDonut() {




        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            data: "{}",
            url: "../api/ATSS/LoadEmployeeAvail",
            success: function (data) {





                var elements = Array();
                elements = data;




                var codelbl = Array();
                var totlbl = Array();


                var i;

                for (i = 0; i < elements.length; i++) {
                    codelbl.push(elements[i].AvailDate);
                    // var getdata = elements[i].totalGrossPay;
                    // var datas = getdata.replace(/,/g, '');


                    //totlbl.push(parseFloat(datas));

                    totlbl.push(elements[i].AvailID);







                }







                var ctx = document.getElementById('donut').getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: codelbl,
                        datasets: [{
                            label: 'Number of Employee Available by Date',
                            data: totlbl,
                            backgroundColor: [
                                'blue',
                                'rgba(54, 162, 235, 0.2)',
                                'rgba(255, 206, 86, 0.2)',
                                'rgba(75, 192, 192, 0.2)',
                                'rgba(153, 102, 255, 0.2)',
                                'violet',
                                'blue',
                                'green',
                                'almond',
                                'maroon',
                                'gold',
                                'pink',
                                'purple',
                                '#ead'
                            ],


                            //borderColor: [
                            //    'black',
                            //       'black',
                            //       'black',
                            //       'black',
                            //       'black',
                            //       'black',
                            //    'black',
                            //      'black',
                            //       'black',
                            //       'black',
                            //        'black',
                            //       'black',
                            //        'black',
                            //         'black'

                            //],
                            //borderWidth: 5
                            hoverOffset: 4
                        },
                        ]
                    },
                    //options: {
                    //    scales: {
                    //        yAxes: [{
                    //            display: true,
                    //            ticks: {
                    //                beginAtZero: true,
                    //                //   min: 0,
                    //                //  max: 100000000,

                    //            }
                    //        }]
                    //    }
                    //}

                });

            }

        });

         }











