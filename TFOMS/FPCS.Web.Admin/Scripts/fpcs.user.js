fpcs.user = {

	initIndexPage: function () {
		fpcs.jqGrid.initGrid({
			gridWrapper: ".gridWrapper",
			gridSelector: "#gridTable",
			pagerSelector: "#gridPager",
			localStorageId: "User",
			showDeleteButton: true,
			url: "/User/_Index",
			sortname: 'SmoRegNum',
			jsonReader: {
				repeatitems: false,
				id: "DbUserId"
			},
			colNames: ['ID', 'Имя', 'Email', 'Роль', 'Заблокирован', 'Реестровый номер СМО','Действия'],
			colModel: [
				{ name: 'DbUserId', index: 'DbUserId', key: true, hidden: true },
				{ name: 'FullName', index: 'FullName', width: 120 },
				{ name: 'Email', index: 'Email', width: 120 },
				{
				    name: 'RoleStr', index: 'Role', width: 70,
					stype: "select",
					searchoptions: {
						value: $("#stypeRole").val(),
						defaultValue: ""
					}
				},
				{
					name: 'IsLocked', index: 'IsLocked', width: 60,
					stype: "select",
					searchoptions: {
					    value: $("#stypeIsLocked").val(),
						defaultValue: ""
					}
				},
				{ name: 'SmoRegNum', index: 'SmoRegNum', width: 60 },
				{ name: 'act', index: 'act', width: 80, fixed: true, sortable: false, resize: false, search: false }
			]
		});

		fpcs.jqGrid.initGridResize();
		fpcs.jqGrid.initFilterToolbar("User");
		fpcs.jqGrid.initNavButtons(null, fpcs.user.showCreateDialog, "Добавить нового пользователя");

		//fpcs.user.initDetailsDialog();
		fpcs.user.initEditDialog();
		fpcs.user.initDeleteOneEntity();
		fpcs.user.initChangeRole();
		fpcs.user.initCreateDialogSend();
		$(window).unload(function () {
			fpcs.jqGrid.saveLocalStorage("User");
		});
	},


	showCreateDialog: function () {
	    fpcs.getPartial('/User/_Create/', function (data, textStatus) {
	        fpcs.showDialog("Добавить новую запись", data);
	        fpcs.user.checkRoleStatus(data);
	        $("#txtLogin").val("");
	        $("#txtPassword").val("");
	    });
	},


	initCreateDialogSend: function () {
	    $(document).on("click", ".createUserSend", function (e) {
	        e.preventDefault();
	        fpcs.sendForm("createUserForm", function (data, textStatus) {
	            if (typeof data === "object" && data.ErrorCode === 200) {
	                fpcs.user.reloadGrid();
	                fpcs.closeDialog();
	            }
	            else {
	                fpcs.showDialog("Создать пользователя", data);
	            }
	        });
	    });
	},

	//showDetailsDialog: function (id) {
	//	fpcs.getPartial('/User/_Details/' + id, function (data, textStatus) {
	//		fpcs.showDialog("User details", data);
	//	});
	//},

	//initDetailsDialog: function () {
	//	$(document).on("click", ".gridRowDetails", function (e) {
	//		var id = $(this).attr("rowid");
	//		fpcs.user.showDetailsDialog(id);
	//	});
	//},

	checkRoleStatus: function (eee) {
	    var role = $("#ddlRole").val();

	    if (role == "User") {
	        //$("#ddlSmo").prop("hiden", false);
	        $("#ddlSmo").show();
	        $("#lblSmo").show();
	    }
	    else {
	        //$("#ddlSmo").prop("hiden", true);
	        $("#ddlSmo").hide();
	        $("#lblSmo").hide();
	    }
	},
    //нет сотрудник да
	initChangeRole: function () {
	    $(document).off("change", "#ddlRole");
	    $(document).on("change", "#ddlRole", function (e, opt) {
	        var role = $("#ddlRole").val();
	        if (role == "User")
	        {
	            //$("#ddlSmo").prop("hiden", false);
	            $("#ddlSmo").show();
	            $("#lblSmo").show();
	        }
	        else
	        {
	            //$("#ddlSmo").prop("hiden", true);
	            //$("#ddlSmo").prop("disabled", true);
	            $("#ddlSmo").hide();
	            $("#lblSmo").hide();
	        }
	    });
	},

	showEditDialog: function (id) {
	    fpcs.getPartial('/User/_Edit/' + id, function (data, textStatus) {
		    fpcs.showDialog("Редактировать пользователя", data);
		    fpcs.user.initIsLockedCheckbox();
		    fpcs.user.checkRoleStatus(data);
		});
	},

	initEditDialog: function () {
	    $(document).on("click", ".gridRowEdit", function (e) {
	        var id = $(this).attr("rowid")
	        fpcs.user.showEditDialog(id);
		});

		$(document).on("click", ".editUserSend", function (e) {
			e.preventDefault();
			fpcs.sendForm("editUserForm", function (data, textStatus) {
				if (typeof data === "object" && data.ErrorCode === 200) {
					fpcs.user.reloadGrid();
					fpcs.closeDialog();
				}
				else {
					fpcs.showDialog("Edit User", data);
				}
			});
		});
	},

	initDeleteOneEntity: function () {
		$(document).off("click", ".gridRowDelete");
		$(document).on("click", ".gridRowDelete", function (e) {
			var id = $(this).attr("rowid");
			var role = $(this).attr("role");
			var url = "/User/Delete/" + id;
			fpcs.executeServiceWithConfirm(url, null, function () {
				fpcs.user.reloadGrid();
			});
		});
	},

	initIsLockedCheckbox: function () {
		$(document).on("change", "#IsLocked", function (e) {
			if ($(this).is(":checked")) {
				$(".isSendEmail").hide();
			} else {
				$(".isSendEmail").show();
			}
		});
	},

	reloadGrid: function () {
		jQuery("#gridTable").trigger("reloadGrid")
	}
}