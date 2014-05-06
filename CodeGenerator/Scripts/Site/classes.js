
 function EntityField(id, name, typeid, datatype, description, mandatory, parentid) {
    this.Id = id;
    this.Name = name;
    this.Description = description;
    this.TypeId = typeid;
    this.DataType = datatype;
    this.Mandatory = mandatory;
    this.ParentId = parentid;
    
    this.UpdateName = function (name) {
        this.Name = name;
    };

    this.UpdateDescription = function (description) {
        this.Description = description;
    };

    this.UpdateDataType = function (datatype) {
        this.DataType = datatype;
    };

    this.UpdateType = function (type){
        this.TypeId = type;
    };

    this.SetMandatory = function (mandatory){
        this.Mandatory = mandatory;
    };

    this.SetParentId = function (parentid) {
        this.ParentId = parentid;
    };

    this.IsNew = function () {
        return this.Id == 0;
    };

    this.IsValid = function() {
        return (this.Name.trim() != "" && this.TypeId > 0 && this.DataType > 0);
    };

    this.ToXML = function() {
        var temp = '<Field><Id>{0}</Id><Name>{1}</Name><Type>{2}</Type><DataType>{3}</DataType><Description>{4}</Description><Mandatory>{5}</Mandatory><ParentEntityId>{6}</ParentEntityId></Field>';
        return temp.Format(new Array(this.Id, this.Name, this.TypeId, this.DataType, this.Description, this.Mandatory, this.ParentId));
    };
}