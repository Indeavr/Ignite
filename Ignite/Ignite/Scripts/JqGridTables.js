﻿function onGridLoaded() {
    jQuery("#grid-user").jqGrid({
        url: "DataProviderForTable",
        datatype: "json",
        height: 'auto',
        width: 800,
        rowNum: 30,
        rowList: [10, 20, 30, 50, 100],
        colNames: ['Index', 'Username', 'CourseName', 'AssignementDate', 'DueDate', 'State', 'Type'],
        colModel: [
            { name: 'Id', key: true, index: 'Id', width: 30, formatter: "integer", search: false },
            { name: 'Username', index: 'Username', width: 120, searchoptions: { sopt: ['eq'] } },
            { name: 'CourseName', index: 'CourseName', width: 120, align: "left", searchoptions: { sopt: ['eq'] } },
            { name: 'DateOfAssignment', index: 'DateOfAssignment', width: 70, align: "left", formatter: "date", search: false },
            { name: 'DueDate', index: 'DueDate', width: 70, align: "left", formatter: "date", search: false },
            { name: 'State', index: 'State', width: 60, align: "left", searchoptions: { sopt: ['eq'] } },
            { name: 'Type', index: 'Type', width: 60, align: "left", searchoptions: { sopt: ['eq'] } }
        ],

        jsonReader: {
            repeatitems: false,
            id: "Index",
            root: 'rows',
            records: 'records',
            page: 'page',
            total: 'total'
        },
        pager: "#pager",
        viewrecords: true,
        sortname: 'Username',
        grouping: true,
        groupingView: {
            groupField: ['Username'],
            groupColumnShow: [true],
            groupText: ['<b>{0} - {1} Item(s)</b>'],
            groupCollapse: true,
            groupOrder: ['desc']
        },
        caption: "Information about all users"
    });
    jQuery("#grid-user").jqGrid('navGrid', '#pager',
        { edit: false, add: false, del: false },
        {},
        {},
        {},
        { multipleSearch: true, multipleGroup: true }
    );
}


