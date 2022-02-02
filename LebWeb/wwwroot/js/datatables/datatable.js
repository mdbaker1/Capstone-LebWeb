function SearchTable() {
    $(document).ready(function () {
        $("#search").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#employeesTable tbody tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        });
    });
}

/*function AddDataTable() {
    $(document).ready(function () {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        var $fileName = 'LebWeb_Employees_Export_' + today

        $("#employeesTable").DataTable({
            "order": [], // disable default sorting on initialization
            "columnDefs": [{ // disable sorting for specific columns
                "targets": [0,1],
                "orderable":false
            }],
            dom: 'Bfrtip',
            lengthMenu: [
                [25, 50, 100, -1],
                ['25 rows', '50 rows', '100 rows', 'Show all']
            ],
            buttons: [
                'pageLength',
                'print',
                {
                    extend: 'collection',
                    text: 'Export As',
                    autoClose: true,
                    buttons: [
                        {
                            extend: 'excelHtml5',
                            title: 'LebWeb_Employees_Export',
                            filename: $fileName
                        },
                        {
                            extend: 'csvHtml5',
                            title: 'LebWeb_Employees_Export',
                            filename: $fileName
                        },
                        {
                            extend: 'pdfHtml5',
                            title: 'LebWeb_Employees_Export',
                            filename: $fileName
                        },
                        'copyHtml5'
                    ]
                }
            ]
        });
    });
}*/

/*function DataTableDispose(table) {
    $(document).ready(function () {
        $(table).DataTable().destroy();
        var element = document.querySelector(table + '_wrapper');
        if (element != null) {
            element.parentNode.removeChild(element);
        }
    });
}*/

function GoToTop100() {
    setTimeout(function () {
        window.location.href = 'top100';
    }, 179000)
}