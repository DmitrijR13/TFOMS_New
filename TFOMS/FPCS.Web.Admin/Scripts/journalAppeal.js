journalAppeal = {

    initIndexPage: function () {
        var dateFrom = $("#txtDateFrom").val();
        var dateTo = $("#txtDateTo").val();
        fpcs.jqGrid.initGrid({
            gridWrapper: ".gridWrapper",
            gridSelector: "#gridTable",
            pagerSelector: "#gridPager",
            localStorageId: "JournalAppeal",
            url: "/JournalAppeal/_Index",
            rowNum: 100,
            showEditButton: false,
            showDeleteButton: false,
            sortname: 'AppealOrganizationCode',
            postData: { dateFromFilter: dateFrom, dateToFilter: dateTo },
            jsonReader: {
                repeatitems: false,
                id: "Id"
            },
            colNames: ['Id', 'Номер', 'Дата обращения', 'Код обращения', 'Вид обращения', 'Сотрудник',
                'Заявитель', 'Застрахованное лицо', 'Направивший орган', 'Дата фактического закрытия обращения', 'СМО', 'Тема обращения',
                'Результат обращения', 'Действия'],
            colModel: [
                { name: 'Id', index: 'Id', key: true, hidden: true },
                 {
                     name: 'AppealUniqueNumber', index: 'AppealUniqueNumber', width: 50,
                     sortable: true, resize: false
                 },
                  {
                      name: 'Date', index: 'Date',
                      sortable: true, width: 40, search: false
                  },
                   {
                       name: 'AppealCode', index: 'AppealCode',
                       sortable: true, width: 50
                   },
                  {
                      name: 'AppealName', index: 'AppealName',
                      sortable: true, width: 50
                  },
                  {
                      name: 'AcceptedBy', index: 'AcceptedBy',
                      sortable: true, width: 150
                  },
                   {
                       name: 'Applicant', index: 'Applicant',
                       sortable: true, width: 100
                   },
                {
                    name: 'ReceivedTreatmentPerson', index: 'ReceivedTreatmentPerson',
                    sortable: true, width: 100
                },
                {
                    name: 'OrganizationsName', index: 'OrganizationsName',
                    sortable: true, width: 100
                },
                {
                    name: 'AppealFactEndDate', index: 'AppealFactEndDate', width: 55,
                    sortable: true, resize: false, search: false
                },
                 {
                     name: 'AppealOrganizationCode', index: 'AppealOrganizationCode',
                     sortable: true, width: 100
                 },              
               
                {
                    name: 'AppealTheme', index: 'AppealTheme', width: 60,
                    sortable: true, resize: false
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
            var FilterPanelCurrentValues = {
                AppealTheme: "",
                AppealUniqueNumber: "",
                AcceptedBy: "",
                Responsible: "",
                AppealCode: "",
                AppealName: "",
                ReceivedTreatmentPerson: "",
                Applicant: "",
                OrganizationsName: "",
                ResultName: "",
                AppealOrganizationCode: ""
            };
            FilterPanelCurrentValues.AppealUniqueNumber = $("#gs_AppealUniqueNumber").val();
            FilterPanelCurrentValues.AppealTheme = $("#gs_AppealTheme").val();
            FilterPanelCurrentValues.AcceptedBy = $("#gs_AcceptedBy").val();
            FilterPanelCurrentValues.Responsible = $("#gs_Responsible").val();
            FilterPanelCurrentValues.AppealCode = $("#gs_AppealCode").val();
            FilterPanelCurrentValues.AppealName = $("#gs_AppealName").val();
            FilterPanelCurrentValues.ReceivedTreatmentPerson = $("#gs_ReceivedTreatmentPerson").val();
            FilterPanelCurrentValues.Applicant = $("#gs_Applicant").val();
            FilterPanelCurrentValues.OrganizationsName = $("#gs_OrganizationsName").val();
            FilterPanelCurrentValues.ResultName = $("#gs_ResultName").val();
            FilterPanelCurrentValues.AppealOrganizationCode = $("#gs_AppealOrganizationCode").val();
            $("#gridTable").setGridParam({ postData: { journalAppealListOptions: FilterPanelCurrentValues, dateFromFilter: $("#txtDateFrom").val(), dateToFilter: $("#txtDateTo").val() } });
            $("#gridTable").trigger("reloadGrid");
        });
    },

    initClearFilter: function () {
        $(document).off("click", ".clearFilter");
        $(document).on("click", ".clearFilter", function (e) {
            $("#txtDateFrom").val("");
            $("#txtDateTo").val("");
            var FilterPanelCurrentValues = {
                AppealTheme: null,
                AppealUniqueNumber: null,
                AcceptedBy: null,
                Responsible: null,
                AppealCode: null,
                AppealName: null,
                ReceivedTreatmentPerson: null,
                Applicant: null,
                OrganizationsName: null,
                ResultName: null,
                AppealOrganizationCode: null
            };
            $("#gs_AppealUniqueNumber").val("");
            $("#gs_AppealTheme").val("");
            $("#gs_AcceptedBy").val("");
            $("#gs_Responsible").val("");
            $("#gs_AppealCode").val("");
            $("#gs_AppealName").val("");
            $("#gs_ReceivedTreatmentPerson").val("");
            $("#gs_Applicant").val("");
            $("#gs_OrganizationsName").val("");
            $("#gs_ResultName").val("");
            $("#gs_AppealOrganizationCode").val("");
            $("#gridTable").setGridParam({ postData: { journalAppealListOptions: FilterPanelCurrentValues, dateFromFilter: $("#txtDateFrom").val(), dateToFilter: $("#txtDateTo").val() } });
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