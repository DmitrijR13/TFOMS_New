report = {

	initIndexPage: function () {
	
	    report.initChangeSmo();
	},


	initChangeSmo: function () {
	    $(document).off("change", "#ddlSmo");
	    $(document).on("change", "#ddlSmo", function (e, opt) {
	        var t = $("#ddlSmo").val();
	        $("#SmoId").val(t);
	    });
	},

	initChangeDateFrom: function () {
	    $(document).off("change", "#txtDateFrom");
	    $(document).on("change", "#txtDateFrom", function (e, opt) {
	        var t = $("#txtDateFrom").val();
	        var t1 = $("#t").val();
	        $("#t").val(t);
	        var t2 = $("#t").val();
	    });
	},

	initChangeDateTo: function () {
	    $(document).off("change", "#txtDateTo");
	    $(document).on("change", "#txtDateTo", function (e, opt) {
	        var t = $("#txtDateTo").val();
	        $("#dateToTemp").val(t);
	    });
	}
}