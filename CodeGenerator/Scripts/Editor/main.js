/* refer to this for jquery controls

http://www.jqueryrain.com/demo/jquery-treeview/

*/

/* Gobal Variables */
var AllEntities = new Array();
var SelectedRows = new Array();
var EntityFields = new Array();
var FieldTypes = { "Simple": 1, "Complex": 2, "Collection":3 };
var DataTypes = {
    1: { "Integer": 1, "Decimal": 2, "String": 3, "Boolean": 4, "Guid": 5 },
    2: { "Entity 1": 1, "Entity 2": 2 },
    3: {"List":1, "Array":2}
};

var entityrowidprefix = 'tr_field_';
var entitycheckboxprefix = 'ckField_';
var currententityid = 0;
var TOP_ROW_ID_SEED = 0;
var selectedrow = null;

var fieldtypetemplate = '<select id="selFieldType_{0}" onchange="EntityFields[\'row_{0}\'].UpdateType(this.value);UpdateDataType({0});">@@val@@</select>';

var entityrowtemplate = '<tr id="{1}{0}" onclick="HighlightRow({0});">';
entityrowtemplate += '<td><input type="checkbox" id="{2}{0}" onclick="SelectEntityRow({0});" value="{0}" /></td>';
entityrowtemplate += '<td><input type="text" onkeypress="EntityFields[\'row_{0}\'].UpdateName(this.value);" id="txtFieldName_{0}" /></td>';
entityrowtemplate += '<td>{3}</td>';
entityrowtemplate += '<td><select id="selFieldDataType_{0}" disabled="disabled" onchange="EntityFields[\'row_{0}\'].UpdateDataType(this.value);"></select></td>';
entityrowtemplate += '<td><input type="text" id="txtFieldDescription_{0}" onkeypress="EntityFields[\'row_{0}\'].UpdateDescription(this.value);" /></td>';
entityrowtemplate += '<td><input type="checkbox" id="txtFieldMandatory_{0}" value="{0}" onclick="MakeFieldMandatory({0});" onchange="EntityFields[\'row_{0}\'].SetMandatory(this.checked);" /></td>';
entityrowtemplate += '</tr>';


/* functions */

function GenerateFieldTypesSelect() {
    var temp = '<option>--</option>';

    for(var x in FieldTypes){
        temp += '<option value="{0}">{1}</option>'.Format([FieldTypes[x], x]);
    }
    return fieldtypetemplate.replace('@@val@@', temp);
}

function GetEntityRowTemplate(rowid) {
    var TypesSelectHTML = GenerateFieldTypesSelect().Format([rowid]);
    return entityrowtemplate.Format([rowid, entityrowidprefix, entitycheckboxprefix, TypesSelectHTML]);
}

function AddEntityRow(tbTable) {
    TOP_ROW_ID_SEED++;
    $('#' + tbTable).append(GetEntityRowTemplate(TOP_ROW_ID_SEED));
    EntityFields["row_" + TOP_ROW_ID_SEED] = new EntityField(TOP_ROW_ID_SEED, "", 0, 0, "", false, currententityid);
}

function UpdateDataType(rowid) {
    var val = $('#selFieldType_' + rowid).val();
    $('#selFieldDataType_' + rowid).find('option').remove().end();

    if (val > 0) {       
        var arr = DataTypes[val];
        $('#selFieldDataType_' + rowid).append('<option value="0">--</option>');

        for (var x in arr) {
            $('#selFieldDataType_' + rowid).append('<option value="{0}">{1}</option>'.Format([arr[x], x]));
        }
        $('#selFieldDataType_' + rowid).prop('disabled', false);
    } else {
        alert("You need to select a field type.");
        $('#selFieldDataType_' + rowid).prop('disabled', true);
    }
}

function RemoveEntityRow(tableid) {
    var selectcount = 0;
    var arrtemp = new Array();

    for (var rowx in SelectedRows) {
        if (SelectedRows[rowx]) {
            $(rowx).remove();
            selectcount++;
        } else
            arrtemp[rowx] = false;
    }
    if (selectcount == 0) {
        alert("You need to select rows to remove");
    } else {
        SelectedRows = arrtemp;
    }
}

function HighlightRow(rowid) {
    $('#' + entityrowidprefix + rowid).css("background-color", "#f99");
}

function SelectEntityRow(rowid) {
   
    var rowsid = '#' + entityrowidprefix + rowid;
    var checkboxid = '#' + entitycheckboxprefix + rowid;

    selectedrow = $(rowsid);
    SelectedRows[rowsid] = $(checkboxid).is(':checked');

    if ($(checkboxid).is(':checked'))
        HighlightRow(rowid);

}

function MakeFieldMandatory(rowid) {

}

function SaveEntity(entityid) {
    var allFieldXML = '';
    var errormsg ="Entity field # {0} is not valid. Please review errors.";

    try{
        for (var entityfield in EntityFields) {
            var field = EntityFields[entityfield];
            
            //grab values from formfields and populate objects.
            if (!field.IsValid()) {
                alert(errormsg.Format([field.Id]));
                return false;
            } else {
                allFieldXML += field.ToXML();
            }
        }

        $('#EntityFieldXML').val(allFieldXML); 
        return true;
    } catch (exception) {
        alert('Sorry could not save entity');
    }
}