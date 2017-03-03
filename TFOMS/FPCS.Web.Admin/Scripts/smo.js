smo = {
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
			localStorageId: "SMO",
			url: "/SMO/_Index",
			rowNum: 100000,
			showEditButton: showEditButton,
			showDeleteButton: showDeleteButton,
			sortname: 'SmoCode',
			jsonReader: {
				repeatitems: false,
				id: "Id"
			},
			colNames: ['Id', 'Код СМО', 'КПП', 'Полное наименование', 'Краткое наименование', 'Фактисекий адрес',
				'ФИО, № телефона, факса, адрес электронной почты руководителя',
				'ФИО, № телефона, факса, адрес электронной почты руководителя обособленного подразделения (филиала)',
				'Сведения о лицензии', 'Действия'],
			colModel: [
				{ name: 'Id', index: 'Id', key: true, hidden: true },
				{
					name: 'SmoCode', index: 'SmoCode', width: 20,
					sortable: true, resize: false
				},
				{
					name: 'KPP', index: 'KPP',
					sortable: true, width: 20
				},
				{
					name: 'FullName', index: 'FullName',
					sortable: true
				},
				{
					name: 'ShortName', index: 'ShortName',
					sortable: true
				},
				{
					name: 'FactAddress', index: 'FactAddress',
					sortable: true
				},
				{
					name: 'Director', index: 'Director',
					sortable: true
				},
				{
					name: 'FilialDirector', index: 'FilialDirector',
					sortable: true
				},
				{
					name: 'LicenseInfo', index: 'LicenseInfo',
					sortable: true
				},
				{ name: 'act', index: 'act', width: 60, fixed: true, sortable: false, resize: false, search: false }
			]
		});

		fpcs.jqGrid.initGridResize();
		fpcs.jqGrid.initFilterToolbar("SMO");
		if (fpcs.getIsAdmin()) {
		    fpcs.jqGrid.initNavButtons("/SMO/DeleteAll", sourceIncome.showCreateDialog, "Добавить новую запись");
		} else {
		    fpcs.jqGrid.initNavButtons(null, null, "");
		}
		//    personEmail.initPersonEmailButton();

		//fpcs.jqGrid.initNavSendEmailButton(fizPerson.initSendEmail);
		// fpcs.jqGrid.initNavSendEmailButton(fizPerson.showSendEmailDialog, "Отправить email");

		//fizPerson.initDetailsDialog();
		smo.initCreateDialogSend();
		smo.initEditDialog();
		smo.initDeleteOneEntity();
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
		fpcs.getPartial('/SMO/_Create/', function (data, textStatus) {
			fpcs.showDialog("Добавить новую запись", data);
		});
	},


	initCreateDialogSend: function () {
		$(document).off("click", ".createSMOSend");
		$(document).on("click", ".createSMOSend", function (e) {
			e.preventDefault();
			fpcs.sendForm("createSMOForm", function (data, textStatus) {
				if (typeof data === "object" && data.ErrorCode === 200) {
					smo.reloadGrid();
					fpcs.closeDialog();
				}
				else {
					fpcs.showDialog("Добавить новую запись", data);
				}
			});
		});
	},

	showEditDialog: function (id) {
		fpcs.getPartial('/SMO/_Edit/' + id, function (data, textStatus) {
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
			smo.showEditDialog(id);
		});

		$(document).off("click", ".editSMOSend");
		$(document).on("click", ".editSMOSend", function (e) {
			e.preventDefault();
			fpcs.sendForm("editSMOForm", function (data, textStatus) {
				if (typeof data === "object" && data.ErrorCode === 200) {
					smo.reloadGrid();
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
			var url = "/SMO/Delete/" + id;
			fpcs.executeServiceWithConfirm(url, null, function () {
				smo.reloadGrid();
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