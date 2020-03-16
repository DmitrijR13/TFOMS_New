handAppeal = {

    initIndexPage: function () {
        var showEditButton = true;
        var showDeleteButton = true;
        if (fpcs.getIsTeacher()) {
            showEditButton = false;
            showDeleteButton = false;
        }
        var dateFrom = $("#txtDateFrom").val();
        var dateTo = $("#txtDateTo").val();
        fpcs.jqGrid.initGrid({
            gridWrapper: ".gridWrapper",
            gridSelector: "#gridTable",
            pagerSelector: "#gridPager",
            //pagerSelectorTop: "#gridPagerTop",
            localStorageId: "HandAppeal",
            url: "/HandAppeal/_Index",
            rowNum: 100,
            showEditButton: showEditButton,
            showDeleteButton: showDeleteButton,
            sortname: 'AppealUniqueNumber',
            postData: { dateFromFilter: dateFrom, dateToFilter: dateTo },
            jsonReader: {
                repeatitems: false,
                id: "Id"
            },
            colNames: ['Id', 'Номер', 'Дата обращения', 'Код обращения', 'Вид обращения', 'Сотрудник', 'Заявитель', 'Застрахованное лицо',
                'Направивший орган', 'Дата фактического закрытия обращения', 'СМО', 'Тема обращения', 'Результат',
                'Файл', 'Файл ответа', '', '', '', 'Действия'],
            colModel: [
                { name: 'Id', index: 'Id', key: true, hidden: true },
                {
                    name: 'AppealUniqueNumber', index: 'AppealUniqueNumber', width: 35,
                    sortable: true, resize: false
                },
                {
                    name: 'Date', index: 'Date',
                    sortable: true, width: 40, search: false
                },
                 {
                     name: 'AppealCode', index: 'AppealCode',
                     sortable: true, width: 100
                 },
                  {
                      name: 'AppealName', index: 'AppealName',
                      sortable: true, width: 100
                  },
                  {
                      name: 'AcceptedBy', index: 'AcceptedBy',
                      sortable: true, width: 100
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
                {
                    name: 'AppealFileName', index: 'AppealFileName', width: 60, fixed: true, sortable: false, resizable: false, search: false, formatter: function (cellvalue, options, rowObject) {
                        var href = "/HandAppeal/GetFile?journalAppealId=" + rowObject["Id"] + "&isAnswer=false";
                        var file = '';
                        if (cellvalue != null && cellvalue != '')
                        {
                            file = '<a href="' + href + '" class="icon-unset">' + cellvalue + '</a>';
                        }
                        
                        return file;
                    }
                },
                 {
                     name: 'AnswerFileName', index: 'AnswerFileName', width: 60, fixed: true, sortable: false, resizable: false, search: false, formatter: function (cellvalue, options, rowObject) {
                         var href = "/HandAppeal/GetFile?journalAppealId=" + rowObject["Id"] + "&isAnswer=true";
                         var file = '';
                         if (cellvalue != null && cellvalue != '') {
                             file = '<a href="' + href + '" class="icon-unset">' + cellvalue + '</a>';
                         }

                         return file;
                     }
                 },
                { name: 'TypeOfAddressingId', index: 'TypeOfAddressingId', hidden: true },
                { name: 'WayOfAddressingId', index: 'WayOfAddressingId', hidden: true },
                { name: 'ThemeAppealCitizensId', index: 'ThemeAppealCitizensId', hidden: true },
                { name: 'act', index: 'act', width: 55, fixed: true, sortable: false, resize: false, search: false }
            ],
            gridComplete: function () {
                var grid = jQuery("#gridTable");          
                var ids = grid.jqGrid('getDataIDs');
                var gridParam = grid.jqGrid("getGridParam")
                for (var i = 0; i < ids.length; i++) {
                    var id = ids[i];
                    var row = grid.getRowData(id);
                    var date = row["Date"];
                    var typeOfAddressingId = row["TypeOfAddressingId"];
                    var wayOfAddressingId = row["WayOfAddressingId"];
                    var themeAppealCitizensId = row["ThemeAppealCitizensId"];
                    var acceptedBy = row["AcceptedBy"];
                    var appealOrganizationCode = row["AppealOrganizationCode"];
                    var appealPlanEndDate = row["AppealPlanEndDate"];
                    var appealFactEndDate = row["AppealFactEndDate"];

                    var appealDate = handAppeal.toDate(date);
                    appealDate.setDate(appealDate.getDate() + 30);

                    var currentDate = new Date();
                    if (!date || (!appealFactEndDate && appealDate < currentDate))
                    {
                        for (var icl = 0; icl < gridParam.colModel.length; icl++) {
                            grid.jqGrid('setCell', id, icl, '', { 'background-color': '#ed7777' });
                        }
                    }
                    else if (!typeOfAddressingId || !wayOfAddressingId || !themeAppealCitizensId
                        || !acceptedBy || !appealOrganizationCode || !appealPlanEndDate)
                    {
                        for (var icl = 0; icl < gridParam.colModel.length; icl++) {
                            grid.jqGrid('setCell', id, icl, '', { 'background-color': '#90ee90' });
                        }
                    }
                    var edit = !showEditButton ? '' : '<td title="Редактировать"><div rowid="' + id + '" class="ui-pg-div gridRowEdit"><span class="ui-icon icon-pencil blue"></span></div></td>';
                    var del = !showDeleteButton ? '' : '<td title="Удалить"><div rowid="' + id + '" class="ui-pg-div gridRowDelete"><span class="ui-icon icon-trash dark"></span></div></td>';

                    var table = '<table class="gridRowActions"><tbody><tr>' + edit + del + '</tr></tbody></table>';
                    grid.jqGrid('setRowData', id, { act: table });

                } 
            },
            ondblClickRow: function (id) {
                $("div.handAppealDetail[rowid='" + id + "']").trigger('click');
            }
        });

        fpcs.jqGrid.initGridResize();
        fpcs.jqGrid.initFilterToolbar("HandAppeal");

        if (!fpcs.getIsTeacher()) {
            fpcs.jqGrid.initNavButtons(null, handAppeal.showCreateDialog, "Добавить новую запись");
            fpcs.jqGrid.initNavButtonsTop(handAppeal.showCreateDialog, "Добавить новую запись");
            
        } else {
            fpcs.jqGrid.initNavButtons(null, null, "");
            fpcs.jqGrid.initNavButtonsTop(null, "")
        }


        
        handAppeal.initCreateDialogSend();
        handAppeal.initEditDialog();
        handAppeal.initDeleteOneEntity();

        handAppeal.initApplyFilter();
        handAppeal.initClearFilter();


        //handAppeal.initGenerateUniqueNumber();

        handAppeal.initPrintReport();

        handAppeal.initPrintLetter();

        handAppeal.initCopyInfoToReceivedTreatment();

        $(document).off("change", ".ddlWayOfAddressing");
        $(document).on("change", ".ddlWayOfAddressing", function (e) {         
            var value = $(".ddlWayOfAddressing option:selected").text();
            if(value == 'Жалоба') {
                var dateFrom = $("#txtDate").val();
                if (dateFrom != null && dateFrom != '') {
                    var newDate = handAppeal.toDate(dateFrom);
                    newDate.setDate(newDate.getDate() + 30);
                    var d = newDate.getDate();
                    var m = newDate.getMonth();
                    m += 1;  // JavaScript months are 0-11
                    m = handAppeal.pad(m, 2);
                    var y = newDate.getFullYear();

                    $("#txtAppealPlanEndDate").val(d + "." + m + "." + y);
                    $("#OZPZRegistrationDateDiv").show();
                }
            }
            else {
                $("#txtAppealPlanEndDate").val('');
                $("#txOZPZRegistrationDate").val('');
                $("#OZPZRegistrationDateDiv").hide();
            }

        });

        $(document).off("change", ".ddlTypeOfAddressing");
        $(document).on("change", ".ddlTypeOfAddressing", function (e) {
            var id = $(".ddlTypeOfAddressing option:selected").val();
            var value = null;
            fpcs.executeService("/HandAppeal/IsUpdateDateEnd/" + id, null, function (data) {
                value = data.Obj;
                if (value) {
                    var dateFrom = $("#txtDate").val();
                    if (dateFrom != null && dateFrom != '') {
                        var newDate = handAppeal.toDate(dateFrom);
                        newDate.setDate(newDate.getDate() + 30);
                        var d = newDate.getDate();
                        var m = newDate.getMonth();
                        m += 1;  // JavaScript months are 0-11
                        m = handAppeal.pad(m, 2);
                        var y = newDate.getFullYear();

                        $("#txtAppealPlanEndDate").val(d + "." + m + "." + y);
                        $("#OZPZRegistrationDateDiv").show();
                    }
                }
                else {
                    $("#txtAppealPlanEndDate").val('');
                    $("#txOZPZRegistrationDate").val('');
                    $("#OZPZRegistrationDateDiv").hide();
                }
            });
        });
    },

    toDate: function (date) {
        var parts = date.split(".");
        return new Date(parts[2], parts[1]-1, parts[0]);
    },

    pad: function (str, max) {
        str = str.toString();
        return str.length < max ? handAppeal.pad("0" + str, max) : str;
    },

    showCreateDialog: function () {
        fpcs.getPartial('/HandAppeal/_Create/', function (data, textStatus) {
            fpcs.showDialog("Добавить новую запись", data);
        });
    },

    initGenerateUniqueNumber: function () {
        $(document).off("click", ".generateUniqueNumber");
        $(document).on("click", ".generateUniqueNumber", function (e) {
            var id = $("#JournalAppealId").val();
            debugger;

            var params = {
                ApplicantSurname: $("#ApplicantSurname").val(),
                ApplicantName: $("#ApplicantName").val(),
                ApplicantSecondName: $("#ApplicantSecondName").val(),
                ReceivedTreatmentPersonSurname: $("#ReceivedTreatmentPersonSurname").val(),
                ReceivedTreatmentPersonName: $("#ReceivedTreatmentPersonName").val(),
                ReceivedTreatmentPersonSecondName: $("#ReceivedTreatmentPersonSecondName").val(),
                Id: id
            };
            
            fpcs.sendForm3('/HandAppeal/GenerateUniqueNumber', params, function (data, textStatus) {
                $("#AppealUniqueNumber").val(data.AppealUniqueNumber);
                $("#UniqueNumberPart1").val(data.UniqueNumberPart1);
                $("#UniqueNumberPart2").val(data.UniqueNumberPart2);
                $("#UniqueNumberPart3").val(data.UniqueNumberPart3);
                debugger;
            });
        });
    },

    initPrintReport: function () {
        $(document).off("click", ".printReport");
        $(document).on("click", ".printReport", function (e) {

            var medOrg = '';
            var medOrgs = $("#ddlMOName option:selected");
            var medOrgsValues = $.map(medOrgs, function (medOrgs) {
                return medOrgs.text;
            });
            if (medOrgsValues != null && medOrgsValues.length > 0) {
                medOrg = medOrgsValues.join("\r\n");
            }

            var org = '';
            var orgs = $("#ddlOrganizationNameList option:selected");
            var orgsValues = $.map(orgs, function (orgs) {
                return orgs.text;
            });
            if (orgsValues != null && orgsValues.length > 0) {
                org = orgsValues.join("\r\n");
            }

            var params = {
                AppealUniqueNumber: $("#AppealUniqueNumber").val(),
                AppealDate: $("#txtDate").val(),
                AppealNumber: $("#AppealNumber").val(),
                OZPZRegistrationDate: $("#txOZPZRegistrationDate").val(),
                AppealOrganizationName: org,
                GuideNumber: $("#GuideNumber").val(),
                GuideDate: $("#txtGuideDate").val(),
                Applicant: $("#ApplicantSurname").val() + ' ' + $("#ApplicantName").val() + ' ' + $("#ApplicantSecondName").val(),
                ApplicantFeedbackAddress: $("#ApplicantFeedbackAddress").val(),
                ApplicantPhoneNumber: $("#ApplicantPhoneNumber").val(),
                ReceivedTreatmentPerson: $("#ReceivedTreatmentPersonSurname").val() + ' ' + $("#ReceivedTreatmentPersonName").val() + ' ' + $("#ReceivedTreatmentPersonSecondName").val(),
                SMOOrganizationName: $(".ddlSMOOrganizationName option:selected").text(),
                WayOfAddressing: $(".ddlWayOfAddressing option:selected").text(),
                OrganizationName: medOrg,
                PassedEvent: $(".ddlPassedEvent option:selected").text(),
                ComplaintName: $(".ddlComplaintName option:selected").text(),
                AppealFactEndDate: $("#txtAppealFactEndDate").val(),
                AppealTheme: $(".ddlAppealTheme option:selected").text(),
                Worker: $(".ddlWorker option:selected").text()
            };
            fpcs.executeService2('/HandAppeal/Print', params);
        });
    },

    initPrintLetter: function () {
        $(document).off("click", ".printLetter");
        $(document).on("click", ".printLetter", function (e) {          
            var params = {
                SMOOrganizationId: $(".ddlSMOOrganizationName option:selected").val(),
                ApplicantSurname: $("#ApplicantSurname").val(),
                ApplicantName: $("#ApplicantName").val(),
                ApplicantSecondName: $("#ApplicantSecondName").val(),
                ApplicantFeedbackAddress: $("#ApplicantFeedbackAddress").val(),
                ApplicantEmail: $("#ApplicantEmail").val(),
                WorkerId: $(".ddlWorker option:selected").val()
            };
            fpcs.executeService2('/HandAppeal/PrintLetter', params);
        });
    },


    initCopyInfoToReceivedTreatment: function () {
        $(document).off("click", ".copyInfoToReceivedTreatment");
        $(document).on("click", ".copyInfoToReceivedTreatment", function (e) {
            $("#ReceivedTreatmentPersonSurname").val($("#ApplicantSurname").val());
            $("#ReceivedTreatmentPersonName").val($("#ApplicantName").val());
            $("#ReceivedTreatmentPersonSecondName").val($("#ApplicantSecondName").val());
            $("#txtReceivedTreatmentPersonBirthDate").val($("#txtApplicantBirthDate").val());
            $("#ReceivedTreatmentPersonENP").val($("#ApplicantENP").val());
        });
    },

    initCreateDialogSend: function () {
        $(document).off("click", "#createHandAppealSend");
        $(document).on("click", "#createHandAppealSend", function (e) {
            e.preventDefault();
            fpcs.sendForm("createHandAppealForm", function (data, textStatus) {
                if (typeof data === "object" && data.ErrorCode === 200) {
                    $('#JournalAppealId').val(data.Obj);
                    
                    handAppeal.reloadGrid();
                    //fpcs.closeDialog();
                }
                else {
                    fpcs.showDialog("Добавить новую запись", data);
                }
            });
        });
    },

    showEditDialog: function (id) {
        fpcs.getPartial('/HandAppeal/_Edit/' + id, function (data, textStatus) {
            fpcs.showDialog("Изменить данные", data);
            var medOrgsArray = $("#MedicalOrganizations").val().split(',');
            var orgsArray = $("#Organizations").val().split(',');
            if (medOrgsArray != null)
            {
                for (var i = 0; i < medOrgsArray.length; i++) {
                    medOrgsArray[i] = medOrgsArray[i].replace(/^\s*/, "").replace(/\s*$/, "");
                }
                $("#ddlMOName").val(medOrgsArray).trigger("chosen:updated");
            }
            if (orgsArray != null) {
                for (var i = 0; i < orgsArray.length; i++) {
                    orgsArray[i] = orgsArray[i].replace(/^\s*/, "").replace(/\s*$/, "");
                }
                $("#ddlOrganizationNameList").val(orgsArray).trigger("chosen:updated");
            }
        });
    },

    initEditDialog: function () {
        $(document).off("click", ".gridRowEdit");
        $(document).on("click", ".gridRowEdit", function (e) {
            var id = $(this).attr("rowid");
            handAppeal.showEditDialog(id);
        });

        $(document).off("click", "#editHandAppealSend");
        $(document).on("click", "#editHandAppealSend", function (e) {
            e.preventDefault();
            fpcs.sendForm("editHandAppealForm", function (data, textStatus) {
                if (typeof data === "object" && data.ErrorCode === 200) {
                    handAppeal.reloadGrid();
                   // fpcs.closeDialog();
                }
                else {
                    fpcs.showDialog("Изменить данные", data);
                }
            });
        });

    },

    initDeleteOneEntity: function () {
        $(document).off("click", ".gridRowDelete");
        $(document).on("click", ".gridRowDelete", function (e) {
            var id = $(this).attr("rowid");
            var url = "/HandAppeal/Delete/" + id;
            fpcs.executeServiceWithConfirm(url, null, function () {
                handAppeal.reloadGrid();
            });
        });
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
            $("#gridTable").setGridParam({ postData: { handAppealListOptions: FilterPanelCurrentValues, dateFromFilter: $("#txtDateFrom").val(), dateToFilter: $("#txtDateTo").val() } });
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
            debugger;
            $("#gridTable").setGridParam({ postData: { handAppealListOptions: FilterPanelCurrentValues, dateFromFilter: $("#txtDateFrom").val(), dateToFilter: $("#txtDateTo").val() } });
            $("#gridTable").trigger("reloadGrid");
        });
    },
    
    reloadGrid: function () {
        jQuery("#gridTable").trigger("reloadGrid")
    }

}