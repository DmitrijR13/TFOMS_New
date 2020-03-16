flk = {

    initIndexPage: function () {
        fpcs.jqGrid.initGrid({
            gridWrapper: ".gridWrapper",
            gridSelector: "#gridTable",
            pagerSelector: "#gridPager",
            localStorageId: "FLK",
            url: "/FLK/_Index",
            rowNum: 100,
            showEditButton: false,
            showDeleteButton: false,
            sortname: 'SmoRegNum',
            jsonReader: {
                repeatitems: false,
                id: "Id"
            },
            colNames: ['Id', 'Реестровый номер СМО', 'Имя исходного файла', 'Код ошибки', 'Имя поля', 'Имя базового элемента',
                'Номер обращения', 'Комментарий'],
            colModel: [
                { name: 'Id', index: 'Id', key: true, hidden: true },
                {
                    name: 'SmoRegNum', index: 'SmoRegNum', width: 20,
                    sortable: true, resize: false
                },
                {
                    name: 'BaseFileName', index: 'BaseFileName',
                    sortable: true, width: 60
                },
                {
                    name: 'ErrorCode', index: 'ErrorCode', width: 20,
                    sortable: true, resize: false
                },
                {
                    name: 'FieldName', index: 'FieldName',
                    sortable: true, width: 30
                },
                {
                    name: 'BaseEntitiyName', index: 'BaseEntitiyName', width: 20,
                    sortable: true, resize: false
                },
                {
                    name: 'AppealNumber', index: 'AppealNumber',
                    sortable: true, width: 60
                },
                {
                    name: 'Comment', index: 'Comment', width: 120,
                    sortable: false, resize: false
                }
            ]           
        });

        fpcs.jqGrid.initGridResize();
        fpcs.jqGrid.initFilterToolbar("FLK");

        fpcs.jqGrid.initNavButtons(null, null, "");  
    },

    reloadGrid: function () {
        jQuery("#gridTable").trigger("reloadGrid")
    }

}