journalAppeal = {

    initIndexPage: function () {
        //var dateFrom = null;
        //var dateTo = null;
        fpcs.jqGrid.initGrid({
            gridWrapper: ".gridWrapper",
            gridSelector: "#gridTable",
            pagerSelector: "#gridPager",
            localStorageId: "JournalAppeal",
            url: "/JournalAppeal/_Index",
            rowNum: 1000,
            showEditButton: false,
            showDeleteButton: false,
            sortname: 'AppealOrganizationCode',
            //postData: { dateFromFilter: dateFrom, dateToFilter: dateTo },
            jsonReader: {
                repeatitems: false,
                id: "Id"
            },
            colNames: ['Id', 'СМО','Уникальный номер обращения', 'Дата обращения', 'Тема обращения', 'Сотрудник, принявший обращение', 'Сотрудник, ответсвенный за обращение',
                'Дата окончания срока рассмотрения обращения', 'Дата фактического закрытия обращения', 'Результат обращения','Действия'],
            colModel: [
                { name: 'Id', index: 'Id', key: true, hidden: true },
                 {
                     name: 'AppealOrganizationCode', index: 'AppealOrganizationCode',
                     sortable: true, width: 100
                 },
                {
                    name: 'AppealUniqueNumber', index: 'AppealUniqueNumber', width: 50,
                    sortable: true, resize: false
                },
                {
                    name: 'Date', index: 'Date',
                    sortable: true, width: 40, search: false
                },
                {
                    name: 'AppealTheme', index: 'AppealTheme', width: 60,
                    sortable: true, resize: false
                },
                {
                    name: 'AcceptedBy', index: 'AcceptedBy',
                    sortable: true, width: 60
                },
                {
                    name: 'Responsible', index: 'Responsible', width: 50,
                    sortable: true, resize: false
                },
                {
                    name: 'AppealPlanEndDate', index: 'AppealPlanEndDate',
                    sortable: true, width: 55, search: false
                },
                {
                    name: 'AppealFactEndDate', index: 'AppealFactEndDate', width: 55,
                    sortable: true, resize: false, search: false
                },
                {
                    name: 'ResultName', index: 'ResultName',
                    sortable: true, width: 60
                },
               
                { name: 'act', index: 'act', width: 30, fixed: true, sortable: false, resize: false, search: false }
            ],
            rowButtons: [
                {
                    title: "Детали", rowClass: "journalAppealDetail", rowIcon: "icon-suitcase green"
                }
            ],

            ondblClickRow: function (id) {
                $("div.journalAppealDetail[rowid='" + id + "']").trigger('click');
            }
        });

        fpcs.jqGrid.initGridResize();
        fpcs.jqGrid.initFilterToolbar("JournalAppeal");

        fpcs.jqGrid.initNavButtons(null, null, "");
        //fpcs.jqGrid.initNavPrintButton2(journalAppeal.initPrint2, 'Report1', 'Report1');
        //fpcs.jqGrid.initNavPrintButton2(journalAppeal.initPrint3, 'Report2', 'Report2');
        //fpcs.jqGrid.initNavPrintButton2(journalAppeal.initPrint4, 'Report3', 'Report3');

        //fpcs.jqGrid.initNavSendEmailButton(fizPerson.initSendEmail);
        // fpcs.jqGrid.initNavSendEmailButton(fizPerson.showSendEmailDialog, "Отправить email");

        //fizPerson.initDetailsDialog();
        journalAppeal.initDetailsDialog();
        //journalAppeal.initEditDialog();
        //journalAppeal.initDeleteOneEntity();
        // fizPerson.sendEmail();

        journalAppeal.initApplyFilter();
        journalAppeal.initClearFilter();
        //$("#txtDateFrom").off('change');
        //$("#txtDateFrom").on('change', function () {
        //    $("#gridTable").setGridParam({ postData: { dateFrom: $("#txtDateFrom").val() } });
        //    $("#gridTable").trigger("reloadGrid");
        //});

        //$("#txtDateTo").off('change');
        //$("#txtDateTo").on('change', function () {
        //    $("#gridTable").setGridParam({ postData: { dateTo: $("#txtDateTo").val() } });
        //    $("#gridTable").trigger("reloadGrid");
        //});
    },

    initApplyFilter: function () {
        $(document).off("click", ".applyFilter");
        $(document).on("click", ".applyFilter", function (e) {
            $("#gridTable").setGridParam({ postData: { dateFromFilter: $("#txtDateFrom").val(), dateToFilter: $("#txtDateTo").val() } });
            $("#gridTable").trigger("reloadGrid");
        });
    },

    initClearFilter: function () {
        $(document).off("click", ".clearFilter");
        $(document).on("click", ".clearFilter", function (e) {
            $("#txtDateFrom").val("");
            $("#txtDateTo").val("");
            $("#gridTable").setGridParam({ postData: { dateFromFilter: $("#txtDateFrom").val(), dateToFilter: $("#txtDateTo").val() } });
            $("#gridTable").trigger("reloadGrid");
        });
    },
    
    showDetailDialog: function (id) {
        fpcs.getPartial('/JournalAppeal/_Detail/' + id, function (data, textStatus) {
            fpcs.showDialog("Подробные данные о обращении", data);
        });
    },

    initDetailsDialog: function () {
        $(document).off("click", ".journalAppealDetail");
        $(document).on("click", ".journalAppealDetail", function (e) {
            var id = $(this).attr("rowid");
            journalAppeal.showDetailDialog(id);
        });
    },

    reloadGrid: function () {
        jQuery("#gridTable").trigger("reloadGrid")
    }

}