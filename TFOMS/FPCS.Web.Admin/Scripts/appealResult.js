﻿appealResult = {

    initIndexPage: function () {
        var showEditButton = true;
        var showDeleteButton = true;
        if (!fpcs.getIsAdmin()) {
            showEditButton = false;
            showDeleteButton = false;
        }
		fpcs.jqGrid.initGrid({
			gridWrapper: ".gridWrapper",
			gridSelector: "#gridTable",
			pagerSelector: "#gridPager",
			localStorageId: "AppealResult",
			url: "/AppealResult/_Index",
			rowNum: 100000,
			showEditButton: showEditButton,
			showDeleteButton: showDeleteButton,
			sortname: 'Code',
			jsonReader: {
				repeatitems: false,
				id: "Id"
			},
			colNames: ['Id', 'Код', 'Наименование', 'Действия'],
			colModel: [
				{ name: 'Id', index: 'Id', key: true, hidden: true },
				{
					name: 'Code', index: 'Code', width: 10,
					sortable: true, resize: false
				},
				{
					name: 'Name', index: 'Name',
					sortable: true
				},
				{ name: 'act', index: 'act', width: 60, fixed: true, sortable: false, resize: false, search: false }
			]
		});

		fpcs.jqGrid.initGridResize();
		fpcs.jqGrid.initFilterToolbar("AppealResult");
		if (fpcs.getIsAdmin()) {
		    fpcs.jqGrid.initNavButtons("/AppealResult/DeleteAll", appealResult.showCreateDialog, "Добавить новую запись");
		} else {
		    fpcs.jqGrid.initNavButtons(null, null, "");
		}

		//    personEmail.initPersonEmailButton();

		//fpcs.jqGrid.initNavSendEmailButton(fizPerson.initSendEmail);
		// fpcs.jqGrid.initNavSendEmailButton(fizPerson.showSendEmailDialog, "Отправить email");

		//fizPerson.initDetailsDialog();
		appealResult.initCreateDialogSend();
		appealResult.initEditDialog();
		appealResult.initDeleteOneEntity();
		// fizPerson.sendEmail();
	},

	//showDetailsDialog: function (id) {
	//    fpcs.getPartial('/Student/_Details/' + id, function (data, textStatus) {
	//        fpcs.showDialog("Student details", data);
	//    });
	//},

	//initDetailsDialog: function () {
	//    $(document).on("click", ".gridRowDetails", function (e) {
	//        var id = $(this).attr("rowid");
	//        fpcs.student.showDetailsDialog(id);
	//    });
	//},

	showCreateDialog: function () {
		fpcs.getPartial('/AppealResult/_Create/', function (data, textStatus) {
			fpcs.showDialog("Добавить новую запись", data);
		});
	},


	initCreateDialogSend: function () {
		$(document).off("click", ".createAppealResultSend");
		$(document).on("click", ".createAppealResultSend", function (e) {
			e.preventDefault();
			fpcs.sendForm("createAppealResultForm", function (data, textStatus) {
				if (typeof data === "object" && data.ErrorCode === 200) {
					appealResult.reloadGrid();
					fpcs.closeDialog();
				}
				else {
					fpcs.showDialog("Добавить новую запись", data);
				}
			});
		});
	},

	showEditDialog: function (id) {
		fpcs.getPartial('/AppealResult/_Edit/' + id, function (data, textStatus) {
			fpcs.showDialog("Изменить данные", data);
		});
	},

	//showSendEmailDialog: function () {
	//    var selRowIds = jQuery('#gridTable').jqGrid('getGridParam', 'selarrrow');
	//    fpcs.getPartial('/Fiz/SendEmail?ids=' + selRowIds, function (data, textStatus) {
	//        fpcs.showDialog("Отправить письма", data);
	//    });
	//},

	initEditDialog: function () {
		$(document).off("click", ".gridRowEdit");
		$(document).on("click", ".gridRowEdit", function (e) {
			var id = $(this).attr("rowid");
			appealResult.showEditDialog(id);
		});

		$(document).off("click", ".editAppealResultSend");
		$(document).on("click", ".editAppealResultSend", function (e) {
			e.preventDefault();
			fpcs.sendForm("editAppealResultForm", function (data, textStatus) {
				if (typeof data === "object" && data.ErrorCode === 200) {
					appealResult.reloadGrid();
					fpcs.closeDialog();
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
			var url = "/AppealResult/Delete/" + id;
			fpcs.executeServiceWithConfirm(url, null, function () {
				appealResult.reloadGrid();
			});
		});
	},

	//sendEmail: function () {
	//    $(document).off("click", ".emailSend");
	//    $(document).on("click", ".emailSend", function (e) {
	//        e.preventDefault();
	//        fpcs.sendForm("sendEmailForm", function (data, textStatus) {
	//            if (typeof data == "object" && data.ErrorCode == 200) {
	//                fizPerson.reloadGrid();
	//                fpcs.closeDialog();
	//            }
	//            else {
	//                fpcs.showDialog("Отправить письма", data);
	//            }
	//        });
	//    });
	//},

	reloadGrid: function () {
		jQuery("#gridTable").trigger("reloadGrid")
	}

}