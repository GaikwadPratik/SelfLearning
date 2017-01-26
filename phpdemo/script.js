let formAction = 'Add';
let deleteInUpdate = {
    'Address':[],
    'ContactNumber':[],
    'EmailAddress':[],
    'University':[],
    'Research':[],
    'Employer':[]
};

let searchCriteria = {
    inTbleId : '',
    inColId : '',
    inColVal : '',
    outTblId1 : '',
    outTblId2 : '',
    outColId1 : '',
    outColId2 : '',
    outColDisplayName1: '',
    outColDisplayName2: ''
};

$(document).ready(function(){
    try{
        makeServerCall('InitialCall',null,generateUserTable);
        clearUserForm('btnReset');
        showFirstDiv();
        $('.date').datepicker();
        initiateTableInformation();
    }
    catch(e){
        console.log(e);
    }
});

function addAddressDiv(){
    try{
        let nCounter = $('[id^="divAddress_"]').filter(':visible').length + 1;
        let stringAddressType = '<option value="">Please select</option>';
        for(let _index = 1,len = jsonAddressTypes.length; _index<len; _index++){
            stringAddressType += '<option value="' + jsonAddressTypes[_index].addressTypeId + '">' + jsonAddressTypes[_index].addressTypeName +'</option>';
        }
        stringAddressType += '<option value="other">other</option>';
        
        let divId = 'divAddress_'+nCounter;
        let stringDiv = '<div id="'+divId+'">'
                                + '<div>'
                                    + '<input type="hidden" name="hdnAddressId'+nCounter+'" id="hdnAddressId'+nCounter+'" value=""/>'
                                + '</div>'
                                + '<div>'
                                    + '<label class="">Address '+nCounter+'</label>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-plus" onclick="addAddressDiv();"></a>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-trash" onclick="hideDynamicDiv(\''+divId+'\');"></a>' 
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">Address Type: </label>'
                                    + '<select style="display:inline-block;" name="ddnAddressType'+nCounter+'" id="ddnAddressType'+nCounter+'" class="form-control dropdown-menu">'
                                        + stringAddressType
                                    + '</select>'
                                    + '<input type="text" class="form-control" style="display:none;" name="txtOtherAddressType'+ nCounter + '" id="txtOtherAddressType'+ nCounter + '" placeholder="Other Address types" />'
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">First Line: </label>'
                                    + '<input type="text" class="form-control" name="txtFirstLine'+ nCounter + '" id="txtFirstLine'+ nCounter + '" placeholder="Please enter avenue number."/>'
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">Second Line: </label>'
                                    + '<input type="text" class="form-control" name="txtSecondLine'+ nCounter + '" id="txtSecondLine'+ nCounter + '" placeholder="Please enter apartment number."/>'
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">City: </label>'
                                    + '<input type="text" class="form-control" name="txtCity'+ nCounter + '" id="txtCity'+ nCounter + '" placeholder="Please enter city in which you reside."/>'
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">State: </label>'
                                    + '<input type="text" class="form-control" name="txtState'+ nCounter + '" id="txtState'+ nCounter + '" maxlength=2 placeholder="Please enter state code in two characters."/>'
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">ZIP: </label>'
                                    + '<input type="text" class="form-control" name="txtZIP'+ nCounter + '" id="txtZIP'+ nCounter + '" maxlength=5 placeholder="Please enter 5 digit zip code." onkeydown="validateNumber(event);"/>'
                                + '</div>'
                            + '</div>';
        $(stringDiv).appendTo('#divAddresses');
        $('#ddnAddressType'+nCounter).selectmenu({
            change:function(event,ui){
                otherTypeHandler(this,ui)
            }
        });
    }
    catch(e){
        console.log(e);
    }
}

function validateNumber(evt) {
    try{
        var e = evt || window.event;
        var key = e.keyCode || e.which;
        if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
        // numbers
        key >= 48 && key <= 57 ||
        // Numeric keypad
        key >= 96 && key <= 105 ||
        // Backspace and Tab and Enter
        key == 8 || key == 9 || key == 13 ||
        // Home and End
        key == 35 || key == 36 ||
        // left and right arrows
        key == 37 || key == 39 ||
        // Del and Ins
        key == 46 || key == 45 ||
        // comma, period and minus, .
        key == 190 || key == 188 || key == 109 || key == 110) {
            // input is VALID
        }
        else {
            // input is INVALID
            e.returnValue = false;
            if (e.preventDefault) e.preventDefault();}
    }
    catch(e){
        console.log(e);
    }
}

function hideDynamicDiv(id){
    try{
        //if update, get the hidden input inside the div and appendTo json object
        if(formAction === 'Update'){
            let hdnId = id.replace('div','hdn').replace('_','Id');
            let propertyName = hdnId.substring(0,hdnId.length-1).replace('hdn','').replace('Id','');
            let primaryKey = $('#'+hdnId).val();
            if(primaryKey && primaryKey !== '')
                deleteInUpdate[propertyName].push(primaryKey);
        }

        $('#'+id).remove();
        showFirstDiv();
    }
    catch(e){
        console.log(e);
    }
}        

function otherTypeHandler(ctrl){
    try{
        let ddn = $(ctrl);
        let ddnId = ddn.prop('id');
        let nCounter = ddnId.substring(ddnId.length-1);
        let otherControlId = ddnId.replace('ddn','txtOther').replace('_','');
        if(ddn.val() === "other"){
            $('#'+otherControlId).show();
        }
        else{
            $('#'+otherControlId).hide();             
        }
    }
    catch(e){
        console.log(e);
    }
}

function addContactNumberDiv(){
    try{
        let nCounter = $('[id^="divContactNumber_"]').filter(':visible').length + 1;
        let stringContactNumberType = '<option value="">Please select</option>';
        for(let _index = 0,len = jsonPhoneType.length; _index<len; _index++){
            stringContactNumberType += '<option value="' + jsonPhoneType[_index].phoneTypesId + '">' + jsonPhoneType[_index].phoneTypesName +'</option>';
        }
        stringContactNumberType += '<option value="other">other</option>';
        let divId = 'divContactNumber_'+nCounter;
        let stringDiv = '<div id="'+divId+'">'
                                + '<div>'
                                    + '<input type="hidden" name="hdnContactNumberId'+nCounter+'" id="hdnContactNumberId'+nCounter+'" value=""/>'
                                + '</div>'
                                + '<div>'
                                    + '<label class="">Contact number '+nCounter+'</label>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-plus" onclick="addContactNumberDiv();"></a>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-trash" onclick="hideDynamicDiv(\''+divId+'\');"></a>' 
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">Phone Type: </label>'
                                    + '<select style="display:inline-block;" name="ddnContactType'+nCounter+'" id="ddnContactType'+nCounter+'" class="form-control dropdown-menu">'
                                        + stringContactNumberType
                                    + '</select>'
                                    + '<input type="text" class="form-control" style="display:none;" name="txtOtherContactType'+ nCounter + '" id="txtOtherContactType'+ nCounter + '" placeholder="Other Contact number types" />'
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">Number: </label>'
                                    + '<input type="text" class="form-control" name="txtContactNumber'+ nCounter + '" id="txtContactNumber'+ nCounter + '" maxlength=12 placeholder="Please enter number in 123.456.7890 format." onkeydown="validateNumber(event);"/>'
                                + '</div>'
                            + '</div>';
        $(stringDiv).appendTo('#divContactNumberss');
        $('#ddnContactType'+nCounter).selectmenu({
            change:function(event,ui){
                otherTypeHandler(this,ui)
            }
        });
    }
    catch(e){
        console.log(e);
    }
}

function addEmailAddressDiv(){
    try{
        let nCounter = $('[id^="divEmailAddress_"]').filter(':visible').length + 1;
        let stringEmailType = '<option value="">Please select</option>';
        for(let _index = 0,len = jsonEmailType.length; _index<len; _index++){
            stringEmailType += '<option value="' + jsonEmailType[_index].emailTypeid + '">' + jsonEmailType[_index].emailTypeName +'</option>';
        }
        stringEmailType += '<option value="other">other</option>';
        let divId = 'divEmailAddress_'+nCounter;
        let stringDiv = '<div id="'+divId+'">'
                                + '<div>'
                                    + '<input type="hidden" name="hdnEmailAddressId'+nCounter+'" id="hdnEmailAddressId'+nCounter+'" value=""/>'
                                + '</div>'
                                + '<div>'
                                    + '<label class="">Email Address '+nCounter+'</label>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-plus" onclick="addEmailAddressDiv();"></a>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-trash" onclick="hideDynamicDiv(\''+divId+'\');"></a>' 
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">Email Type: </label>'
                                    + '<select style="display:inline-block;" name="ddnEmailType'+nCounter+'" id="ddnEmailType'+nCounter+'" class="form-control dropdown-menu">'
                                        + stringEmailType
                                    + '</select>'
                                    + '<input type="text" class="form-control" style="display:none;" name="txtOtherEmailType'+ nCounter + '" id="txtOtherEmailType'+ nCounter + '" placeholder="Other Contact number types" />'
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">Email: </label>'
                                    + '<input type="email" class="form-control" name="txtEmailAddress'+ nCounter + '" id="txtEmailAddress'+ nCounter + '" placeholder="Please enter your email address."/>'
                                + '</div>'
                            + '</div>';
        $(stringDiv).appendTo('#divEmailAddresses');
        $('#ddnEmailType'+nCounter).selectmenu({
            change:function(event,ui){
                otherTypeHandler(this,ui)
            }
        });
    }
    catch(e){
        console.log(e);
    }
}

function addEducationDiv(){
    try{
        let nCounter = $('[id^="divUniversity_"]').filter(':visible').length + 1;
        let stringUniversityType = '<option value="">Please select</option>';
        for(let _index = 0,len = jsonUniversityType.length; _index<len; _index++){
            stringUniversityType += '<option value="' + jsonUniversityType[_index].universitytypeId + '">' + jsonUniversityType[_index].universitytypeName +'</option>';
        }
        stringUniversityType += '<option value="other">other</option>';
        let divId = 'divUniversity_'+nCounter;
        let stringDiv = '<div id="'+divId+'">'
                                + '<div>'
                                    + '<input type="hidden" name="hdnUniversityId'+nCounter+'" id="hdnUniversityId'+nCounter+'" value=""/>'
                                + '</div>'
                                + '<div>'
                                    + '<label class="">University '+nCounter+'</label>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-plus" onclick="addEducationDiv();"></a>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-trash" onclick="hideDynamicDiv(\''+divId+'\');"></a>' 
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">University Type: </label>'
                                    + '<select style="display:inline-block;" name="ddnUniversityType'+nCounter+'" id="ddnUniversityType'+nCounter+'" class="form-control dropdown-menu">'
                                        + stringUniversityType
                                    + '</select>'
                                    + '<input type="text" class="form-control" style="display:none;" name="txtOtherUniversityType'+ nCounter + '" id="txtOtherUniversityType'+ nCounter + '" placeholder="Other Contact number types" />'
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">University Name: </label>'
                                    + '<input type="text" class="form-control" name="txtUniversityName'+ nCounter + '" id="txtUniversityName'+ nCounter + '" placeholder="Please enter university name."/>'
                                + '</div>'
                                + '<p>From Date: <input class="date" type="date" name="txtFromUniversityDate' + nCounter + '" id="txtFromUniversityDate' + nCounter + '" placeholder="Please select joining date."></p>'
                                + '<div class="checkbox"><label> <input type="checkbox" id="chkCurrentUniveristy' + nCounter + '" id="chkCurrentUniversity' + nCounter + '" onclick="processToDate(this,\'txtToUniversityDate'+nCounter+'\')" />Currently attending</label></div>'
                                + '<p>To Date: <input class="date" type="date" name="txtToUniversityDate' + nCounter + '" id="txtToUniversityDate' + nCounter + '" placeholder="Please select leaving date."></p>'
                            + '</div>';
        $(stringDiv).appendTo('#divUniversities');
        $('#divUniversities .date').datepicker();
        $('#ddnUniversityType'+nCounter).selectmenu({
            change:function(event,ui){
                otherTypeHandler(this,ui)
            }
        });
    }
    catch(e){
        console.log(e);
    }
}

function processToDate(ctrl, txtId){
    try{
        if($(ctrl).is(':checked'))
            $('#'+txtId).val('').parent('p').hide();
        else
            $('#'+txtId).parent('p').show();
    }
    catch(e){
        console.log(e);
    }
}

function addEmployerDiv(){
    try{
        let nCounter = $('[id^="divEmployer_"]').filter(':visible').length + 1;
        
        let divId = 'divEmployer_'+nCounter;
        let stringDiv = '<div id="'+divId+'">'
                                + '<div>'
                                    + '<input type="hidden" name="hdnEmployerId'+nCounter+'" id="hdnEmployerId'+nCounter+'" value=""/>'
                                + '</div>'
                                + '<div>'
                                    + '<label class="">Employer '+nCounter+'</label>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-plus" onclick="addEmployerDiv();"></a>'
                                    + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-trash" onclick="hideDynamicDiv(\''+divId+'\');"></a>' 
                                + '</div>'
                                + '<div class="form-group input-group">'
                                    + '<label class="input-group-addon">Employer Name: </label>'
                                    + '<input type="text" class="form-control" name="txtEmployerName'+ nCounter + '" id="txtEmployerName'+ nCounter + '" />'
                                + '</div>'
                                + '<p>From Date: <input class="date" type="date" name="txtFromEmployerDate' + nCounter + '" id="txtFromEmployerDate' + nCounter + '"></p>'
                                + '<div class="checkbox"><label> <input type="checkbox" id="chkCurrentEmployer' + nCounter + '" id="chkCurrentEmployer' + nCounter + '" onclick="processToDate(this,\'txtToEmployerDate'+nCounter+'\')" />Currently attending</label></div>'
                                + '<p>To Date: <input class="date" type="date" name="txtToEmployerDate' + nCounter + '" id="txtToEmployerDate' + nCounter + '"></p>'
                            + '</div>';
        $(stringDiv).appendTo('#divEmployers');
        $('#divEmployers .date').datepicker();
    }
    catch(e){
        console.log(e);
    }
}

function addResearchDiv(){
    try{
        let nCounter = $('[id^="divResearch_"]').filter(':visible').length + 1;
        
        let divId = 'divResearch_'+nCounter;
        let stringDiv = '<div id="'+divId+'">'
                            + '<div>'
                                    + '<input type="hidden" name="hdnResearchId'+nCounter+'" id="hdnResearchId'+nCounter+'" value=""/>'
                            + '</div>'
                            + '<div>'
                                + '<label class="">Research Topic '+nCounter+'</label>'
                                + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-plus" onclick="addResearchDiv();"></a>'
                                + '<a style="float:right" href="javascript:void(0);" class="glyphicon glyphicon-trash" onclick="hideDynamicDiv(\''+divId+'\');"></a>' 
                            + '</div>'
                            + '<div class="form-group input-group">'
                                + '<label class="input-group-addon">Topic Name: </label>'
                                + '<input type="text" class="form-control" name="txtTopicName'+ nCounter + '" id="txtTopicName'+ nCounter + '" />'
                            + '</div>'
                            + '<div class="form-group input-group">'
                                + '<label class="input-group-addon">Topic Desc: </label>'
                                + '<input type="text" class="form-control" name="txtTopicDesc'+ nCounter + '" id="txtTopicDesc'+ nCounter + '" />'
                            + '</div>'
                        + '</div>';
        $(stringDiv).appendTo('#divResearches');        
    }
    catch(e){
        console.log(e);
    }
}

function showFirstDiv(){
    try{
        let totalAddressDivs = $('[id^="divAddress_"]').filter(':visible').length;
        if(totalAddressDivs <= 0){
            addAddressDiv();
        }

        let totalContactDivs = $('[id^="divContactNumber_"]').filter(':visible').length;
        if(totalContactDivs <= 0){
            addContactNumberDiv();
        }

        let totalEmailDivs = $('[id^="divEmailAddress_"]').filter(':visible').length;
        if(totalEmailDivs <= 0){
            addEmailAddressDiv();
        }

        let totalUniversityDivs = $('[id^="divUniversity_"]').filter(':visible').length;
        if(totalUniversityDivs <= 0){
            addEducationDiv();
        }
    }
    catch(e){
        console.log(e);
    }
}

function addPerson(){
    //TODO:: add vaidations
    try{
        let personalData = {};
        personalData['personId'] = $('#hdnPersonId').val();
        personalData['firstName'] = $('#txtFirstName').val();
        personalData['lastName'] = $('#txtLastName').val();
        personalData['sex'] = $('#ddnSex').val();

        let addresses = [];
        let visibleAddressesDivs = $('[id^="divAddress_"]').filter(':visible');
        for(let index = 0, len = visibleAddressesDivs.length; index < len; index++){
            let addressData = {};
            let addressDiv = visibleAddressesDivs[index];
            let divId = addressDiv['id'];
            let nAddressDivCounter = divId.substring(divId.length - 1);
            addressData['addressId'] = $('#hdnAddressId'+nAddressDivCounter).val();
            addressData['addressType'] = $('#'+divId).find('#ddnAddressType'+nAddressDivCounter).val();
            if(addressData['addressType'] === 'other')
                addressData['addressTypeName'] = $('#txtOtherAddressType'+nAddressDivCounter).val();
            addressData['firstLine'] = $('#'+divId).find('#txtFirstLine'+nAddressDivCounter).val();
            addressData['secondLine'] = $('#'+divId).find('#txtSecondLine'+nAddressDivCounter).val();
            addressData['city'] = $('#'+divId).find('#txtCity'+nAddressDivCounter).val();
            addressData['state'] = $('#'+divId).find('#txtState'+nAddressDivCounter).val();
            addressData['zip'] = $('#'+divId).find('#txtZIP'+nAddressDivCounter).val();
            addresses.push(addressData);                
        }

        let visibleContactDivs = $('[id^="divContactNumber_"]').filter(':visible');
        let contacts = [];
        for(let index = 0, len = visibleContactDivs.length; index < len; index++){
            let phoneData = {};
            let ContactDiv = visibleContactDivs[index];
            let divId = ContactDiv['id'];
            let nAddressDivCounter = divId.substring(divId.length - 1);
            phoneData['phonenoId'] = $('#hdnContactNumberId'+nAddressDivCounter).val();
            phoneData['contactType'] = $('#'+divId).find('#ddnContactType'+nAddressDivCounter).val();
            if(phoneData['contactType'] === 'other')
                phoneData['contactTypeName'] = $('#txtOtherContactType'+nAddressDivCounter).val();
            phoneData['number'] = $('#'+divId).find('#txtContactNumber'+nAddressDivCounter).val();    
            contacts.push(phoneData);                
        }

        let visibleEmailDivs = $('[id^="divEmailAddress_"]').filter(':visible');
        let emails = [];
        for(let index = 0, len = visibleEmailDivs.length; index < len; index++){
            let emailData = {};
            let emailDiv = visibleEmailDivs[index];
            let divId = emailDiv['id'];
            let nAddressDivCounter = divId.substring(divId.length - 1);
            emailData['id'] = $('#hdnEmailAddressId'+nAddressDivCounter).val();
            emailData['emailType'] = $('#'+divId).find('#ddnEmailType'+nAddressDivCounter).val();
            if(emailData['emailType'] === 'other')
                emailData['emailTypeName'] = $('#txtOtherEmailType'+nAddressDivCounter).val();
            emailData['email'] = $('#'+divId).find('#txtEmailAddress'+nAddressDivCounter).val();    
            emails.push(emailData);                
        }

        let visibleUniversityDivs = $('[id^="divUniversity_"]').filter(':visible');
        let universities = [];
        for(let index = 0, len = visibleUniversityDivs.length; index < len; index++){
            let universityData = {};
            let universityDiv = visibleUniversityDivs[index];
            let divId = universityDiv['id'];
            let nAddressDivCounter = divId.substring(divId.length - 1);
            universityData['universityId'] = $('#hdnUniversityId'+nAddressDivCounter).val();
            universityData['universityType'] = $('#'+divId).find('#ddnUniversityType'+nAddressDivCounter).val();
            if(universityData['universityType'] === 'other')
                universityData['universityTypeName'] = $('#txtOtherUniversityType'+nAddressDivCounter).val();
            universityData['universityName'] = $('#'+divId).find('#txtUniversityName'+nAddressDivCounter).val();
            universityData['fromDate'] = $('#'+divId).find('#txtFromUniversityDate'+nAddressDivCounter).val();
            universityData['toDate'] = $('#'+divId).find('#txtToUniversityDate'+nAddressDivCounter).val();
            universities.push(universityData);                
        }

        let visibleEmployerDivs = $('[id^="divEmployer_"]').filter(':visible');
        let employers = [];
        for(let index = 0, len = visibleEmployerDivs.length; index < len; index++){
            let employerData = {};
            let empoyerDiv = visibleEmployerDivs[index];
            let divId = empoyerDiv['id'];
            let nAddressDivCounter = divId.substring(divId.length - 1);
            employerData['employersId'] = $('#hdnEmployerId'+nAddressDivCounter).val();
            employerData['employerName'] = $('#'+divId).find('#txtEmployerName'+nAddressDivCounter).val();
            employerData['fromDate'] = $('#'+divId).find('#txtFromEmployerDate'+nAddressDivCounter).val();
            employerData['toDate'] = $('#'+divId).find('#txtToEmployerDate'+nAddressDivCounter).val();
            employers.push(employerData);                
        }

        let visibleResearchDivs = $('[id^="divResearch_"]').filter(':visible');
        let reseraches = [];
        for(let index = 0, len = visibleResearchDivs.length; index < len; index++){
            let researchData = {};
            let researchDiv = visibleResearchDivs[index];
            let divId = researchDiv['id'];
            let nAddressDivCounter = divId.substring(divId.length - 1);
            researchData['researcheId'] = $('#hdnResearchId'+nAddressDivCounter).val();
            researchData['researchName'] = $('#'+divId).find('#txtTopicName'+nAddressDivCounter).val();
            researchData['researchDesc'] = $('#'+divId).find('#txtTopicDesc'+nAddressDivCounter).val();
            reseraches.push(researchData);                
        }

        let serverData = {};
        serverData['person'] = personalData;
        serverData['addresses'] = addresses;
        serverData['phonenos'] = contacts;
        serverData['emails'] = emails;
        serverData['universities'] = universities;
        if(employers.length > 0)
            serverData['employers'] = employers;
        if(reseraches.length > 0)
            serverData['researches'] = reseraches;
        
        showViewData(serverData,true);
    }
    catch(e){
        console.log(e);
    }
}

function clearUserForm(id){
    try{
        if(arguments.callee.caller.name === '' || formAction === 'Add')
            formAction = 'Add';
        else
            formAction = 'Update';
        if(formAction === 'Update'){
            editUser($('#hdnPersonId').val());
            $('.dropdown-menu').selectmenu('refresh');
        }
        else{
            let form = $('#'+id).closest('form');
            form.find("input[type=text], textarea, select, input[type=email], input[type=date]").val("");
            formAction = 'Add';
            $('#addUser').text('Add User');
        }
    }
    catch(e){
        console.log(e);
    }
}

function showAddUser(bShow){
    try{
        //dialog.dialog("open");
        if(bShow){
            $('#allUsers, #divAddUser').addClass('none');
            $('#addForm').removeClass('none').addClass('show');
            $('#ddnSex').selectmenu();                
        }
        else{
            // $('#allUsers, #divAddUser').removeClass('none');
            // $('#addForm').removeClass('show').addClass('none');
            location.reload(true);
        }
    }
    catch(e){
        console.log(e);
    }
}

function showViewData(data,confirmation){
    try{
        if(data){
            let personData = data['person'];
            if(personData){
                let personString = '<h3>Personal Information</h3>'
                                + '<table style="margin-left:auto;margin-right:auto;text-align:left;border-style:solid">'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">First Name: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ personData['firstName'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Last Name: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ personData['lastName'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Sex: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value ="'+ (personData['sex'] === 'M' ? 'Male' : 'Female') + '" disabled/>';
                                        + '</td>'
                                    + '</tr>'
                                + '</table>';
                $(personString).appendTo('#viewData');
            }

            let addresses = data['addresses'];
            if(addresses && addresses.length > 0){
                let addressString = '<h3>Address Information</h3>';
                for(let index=0,len=addresses.length;index<len;index++){
                    let addressData = addresses[index];
                    let addressType = '';                
                    for(let tempIndex = 0;tempIndex<jsonAddressTypes.length;tempIndex++){
                        if(jsonAddressTypes[tempIndex].addressTypeId === addressData['addressType']){
                            addressType = jsonAddressTypes[tempIndex].addressTypeName;
                            break;
                        }
                    }

                    let otherAddressTypeString = '';
                    if(addressData['addressType'] === 'other')
                    {
                        otherAddressTypeString = '<tr>'
                                                    + '<td>'
                                                        + '<label class="input-group-addon dialogLable">New Address Type: </label>'
                                                    + '</td>'
                                                    + '<td>'
                                                        + '<input type="text" value="'+ (addressData['addressTypeName']) + '" disabled/>'
                                                    + '</td>'
                                                + '</tr>'
                    }

                    addressString += '<h4 style="text-align:center">Address ' + (index +1) + '<h4>'
                                + '<table style="margin-left:auto;margin-right:auto;text-align:left;border-style:solid">'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Address Type: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ (addressData['addressType'] === 'other' ? 'Other' : addressType ) + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + otherAddressTypeString
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">First Line: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ addressData['firstLine'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Second Line: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ addressData['secondLine'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">City: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ addressData['city'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">State: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ addressData['state'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Zip: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ addressData['zip'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                + '</table>'
                }
                $(addressString).appendTo('#viewData');
            }
            
            let contacts = data['phonenos'];
            if(contacts && contacts.length > 0){
                let contactString = '<h3>Contact Information</h3>';
                for(let index=0,len=contacts.length;index<len;index++){
                    let contacData = contacts[index];
                    let contactType = '';                
                    for(let tempIndex = 0;tempIndex<jsonPhoneType.length;tempIndex++){
                        if(jsonPhoneType[tempIndex].phoneTypesId === contacData['contactType']){
                            contactType = jsonPhoneType[tempIndex].phoneTypesName;
                            break;
                        }
                    }

                    let otherContactTypeString = '';
                    if(contacData['contactType'] === 'other')
                    {
                        otherContactTypeString = '<tr>'
                                                    + '<td>'
                                                        + '<label class="input-group-addon dialogLable">New Contact Type: </label>'
                                                    + '</td>'
                                                    + '<td>'
                                                        + '<input type="text" value="'+ (contacData['contactTypeName']) + '" disabled/>'
                                                    + '</td>'
                                                + '</tr>'
                    }

                    contactString += '<h4 style="text-align:center">Contact number ' + (index +1) + '<h4>'
                                + '<table style="margin-left:auto;margin-right:auto;text-align:left;border-style:solid">'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Contact Type: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ (contacData['contactType'] === 'other' ? 'Other' : contactType ) + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + otherContactTypeString
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Number: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ contacData['number'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                + '</table>'                
                }
                $(contactString).appendTo('#viewData');
            }

            let emails = data['emails'];
            if(emails && emails.length > 0){
                let emailString = '<h3>Email Information</h3>';
                for(let index=0,len=emails.length;index<len;index++){
                    let emailData = emails[index];
                    let emailType = '';                
                    for(let tempIndex = 0;tempIndex<jsonEmailType.length;tempIndex++){
                        if(jsonEmailType[tempIndex].emailTypeid === emailData['emailType']){
                            emailType = jsonEmailType[tempIndex].emailTypeName;
                            break;
                        }
                    }

                    let otherEmailTypeString = '';
                    if(emailData['emailType'] === 'other')
                    {
                        otherEmailTypeString = '<tr>'
                                                    + '<td>'
                                                        + '<label class="input-group-addon dialogLable">New Email Type: </label>'
                                                    + '</td>'
                                                    + '<td>'
                                                        + '<input type="text" value="'+ (emailData['emailTypeName']) + '" disabled/>'
                                                    + '</td>'
                                                + '</tr>'
                    }

                    emailString += '<h4 style="text-align:center">Email Id ' + (index +1) + '<h4>'
                                + '<table style="margin-left:auto;margin-right:auto;text-align:left;border-style:solid">'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Email Type: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ (emailData['emailType'] === 'other' ? 'Other' : emailType ) + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + otherEmailTypeString
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Email: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ emailData['email'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                + '</table>'                
                }
                $(emailString).appendTo('#viewData');
            }

            let universities = data['universities'];
            if(universities && universities.length > 0){
                let universityString = '<h3>Education Information</h3>';
                for(let index=0,len=universities.length;index<len;index++){
                    let universityData = universities[index];
                    let universityType = '';                
                    for(let tempIndex = 0;tempIndex<jsonUniversityType.length;tempIndex++){
                        if(jsonUniversityType[tempIndex].universitytypeId === universityData['universityType']){
                            universityType = jsonUniversityType[tempIndex].universitytypeName;
                            break;
                        }
                    }

                    let otherUniversityTypeString = '';
                    if(universityData['universityType'] === 'other')
                    {
                        otherUniversityTypeString = '<tr>'
                                                    + '<td>'
                                                        + '<label class="input-group-addon dialogLable">New University Type: </label>'
                                                    + '</td>'
                                                    + '<td>'
                                                        + '<input type="text" value="'+ (universityData['universityTypeName']) + '" disabled/>'
                                                    + '</td>'
                                                + '</tr>'
                    }
                    
                    universityString += '<h4 style="text-align:center">University ' + (index +1) + '<h4>'
                                + '<table style="margin-left:auto;margin-right:auto;text-align:left;border-style:solid">'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">University Type: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ (universityData['universityType'] === 'other' ? 'Other' : universityType ) + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + otherUniversityTypeString
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">University Name: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ universityData['universityName'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">From: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ universityData['fromDate'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">To: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ ((universityData['toDate'] === null || universityData['toDate'] === '') ? 'Currently attending' : universityData['toDate']) + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                + '</table>'
                }
                $(universityString).appendTo('#viewData');
            }

            let researches = data['researches'];
            if(researches && researches.length > 0){
                let researcheString = '<h3>Research Topics</h3>';
                for(let index=0,len=researches.length;index<len;index++){
                    let researchData = researches[index];
                    researcheString += '<h4 style="text-align:center">Research ' + (index +1) + '<h4>'
                                + '<table style="margin-left:auto;margin-right:auto;text-align:left;border-style:solid">'                                        
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Topic Name: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ researchData['researchName'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Topic description: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ researchData['researchDesc'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                + '</table>'
                }
                $(researcheString).appendTo('#viewData');
            }

            let employers = data['employers'];
            if(employers && employers.length > 0){
                let employerString = '<h3>Employment History</h3>';
                for(let index=0,len=employers.length;index<len;index++){
                    let employerData = employers[index];
                    employerString += '<h4 style="text-align:center">Employer ' + (index +1) + '<h4>'
                                + '<table style="margin-left:auto;margin-right:auto;text-align:left;border-style:solid">'                                        
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">Employer Name: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ employerData['employerName'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">From: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ employerData['fromDate'] + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                    + '<tr>'
                                        + '<td>'
                                            + '<label class="input-group-addon dialogLable">To: </label>'
                                        + '</td>'
                                        + '<td>'
                                            + '<input type="text" value="'+ ((employerData['toDate'] === null || employerData['toDate'] === '') ? 'Currently working' : employerData['toDate']) + '" disabled/>'
                                        + '</td>'
                                    + '</tr>'
                                + '</table>'
                }
                $(employerString).appendTo('#viewData');
            }

            if($('#viewData').html().trim() !== '')
                openDialog(data,confirmation);
            else
                alert('No data to display');
        }
    }
    catch(e){
        console.log(e);
    }
}

function openDialog(data,confirmation){
    try{
        let returnVal = false;
        let title = confirmation ? 'Record Confirmation' : 'Record Information';
        
        let buttons = [];
        let cancelButton = 
        {
            label: confirmation ? 'Cancel' : 'Close',
            id: 'btnDialogCancel',
            icon: 'glyphicon glyphicon-ban-circle',
            cssClass: "btn btn-warning",
            action: function(dialogRef) {
                dialogRef.close();
            }
        }
        buttons.push(cancelButton);  
        if(confirmation){
            let confirmButton =
            {
                label: 'Confirm',
                id: 'btnDialogConfirm',
                cssClass: "btn btn-success",
                action: function(dialogRef) {
                    let methodName = 'InsertNewRecord';
                    if(formAction === 'Update'){
                        methodName = 'UpdateRecord';
                        data['ToBeDeleted'] = deleteInUpdate;
                        makeServerCall(methodName,data,callBackTest);
                    }
                    else if(formAction === 'Search'){
                        methodName = 'DynamicSearch';                
                        makeServerCall(methodName,data,displaySearchResults);
                    }
                    else
                        makeServerCall(methodName,data,callBackTest);
                        
                    dialogRef.close();
                    //if(formAction !== 'Search')
                        
                }
            }
        
            buttons.push(confirmButton);
        }
        
        let dialog = new BootstrapDialog
                    ({
                        size: BootstrapDialog.SIZE_LARGE,
                        title: title,
                        message: $('#viewData').html(),
                        closable: false,
                        buttons: buttons
                    });
        dialog.realize();
        
        // if(confirmation){
        //    var btn1 = dialog.getButton('btnDialogConfirm');
        //    $(btn1).on('click',function(){
        //        return returnVal = true;
        //    });
        // }
        dialog.open();
        $('#viewData').html('');
    }
    catch(e){
        console.log(e);
    }
}

function callBackTest(responseData){    
    location.reload(true);
}

function makeServerCall(method,data,callBack){
    try{
        $.ajax({
                type: "POST",
                url: "ajaxCallHandler.php",
                data: {method:method, serverData:JSON.stringify(data)},
                success: callBack,
                error: function(xhr,stataus,error){
                    console.log(error);
                }
        });
    }
    catch(e){
        console.log(e);
    }
}

function viewUser(personId){
    try{
        let serverData = {};
        serverData['personId'] = personId;
        makeServerCall("GetUser",serverData,viewUserProcess);
    }
    catch(e){
        console.log(e);
    }
}

function viewUserProcess(responseData){
    try{
        let data = JSON.parse(responseData);
        showViewData(data, false);    
    }
    catch(e){
        console.log(e);
    }
}

function deleteUser(personId){
    try{
        let serverData = {};
        serverData['personId'] = personId;
        makeServerCall("DeleteUser",serverData,function(){
            alert('User deleted successfully.');
            location.reload(true);
        });
    }
    catch(e){
        console.log(e);
    }
}

function editUser(personId){
    try{
        let serverData = {};
        serverData['personId'] = personId;
        makeServerCall("GetUser",serverData,editUserProcess);
    }
    catch(e){
        console.log(e);
    }
}

function editUserProcess(responseData){//TODO:: reset is not working when previous div in any element is deleted when they have more than 1 in Update.
    try{
        let data = JSON.parse(responseData);
        let person = data['person'];
        if(person){
            showAddUser(true);

            let strAddress = "divAddress_";
            let strPhone = "divContactNumber_";
            let strEmail = "divEmailAddress_";
            let strUniversity = "divUniversity_";
            let strResearch = "divResearch_";
            let strEmployer = "divEmployer_";

            $('#hdnPersonId').val(person.personId);
            $('#txtFirstName').val(person.firstName);
            $('#txtLastName').val(person.lastName);
            $('#ddnSex').val(person.sex);

            let addresses = data['addresses'];
            if(addresses && addresses.length > 0){
                for(let index=0,len = addresses.length;index<len;index++){
                    let counter = index +1;
                    let address = addresses[index];
                    if(address){
                        if(index > 0)
                            addAddressDiv(address.addressId);
                        $('#hdnAddressId'+counter).val(address.addressId);
                        $('#ddnAddressType'+ counter).val(address.addressType);
                        $('#txtFirstLine'+ counter).val(address.firstLine);
                        $('#txtSecondLine'+ counter).val(address.secondLine);
                        $('#txtCity'+ counter).val(address.city);
                        $('#txtState'+ counter).val(address.state);
                        $('#txtZIP'+ counter).val(address.zip);
                    }
                }
                
                let len = $('[id^='+strAddress+']').filter(':visible').length;
                for(let index=addresses.length;index<len;index++){
                    hideDynamicDiv(strAddress+(index+1));
                }
            }

            let contacts = data['phonenos'];
            if(contacts && contacts.length>0){
                for(let index=0,len=contacts.length;index<len;index++){
                    let counter = index+1;
                    let contact = contacts[index];
                    if(contact){
                        if(index > 0)
                            addContactNumberDiv();
                        $('#hdnContactNumberId'+counter).val(contact.phonenoId);
                        $('#ddnContactType'+ counter).val(contact.contactType);
                        $('#txtContactNumber'+ counter).val(contact.number);
                    }
                }
                
                len = $('[id^='+strPhone+']').filter(':visible').length;
                for(let index=contacts.length;index<len;index++){
                    hideDynamicDiv(strPhone+(index+1));
                }
            }

            let emails = data['emails'];
            if(emails && emails.length>0){
                for(let index=0,len=emails.length;index<len;index++){
                    let counter = index+1;
                    let email = emails[index];
                    if(email){
                        if(index > 0)
                            addEmailAddressDiv();
                        $('#hdnEmailAddressId'+counter).val(email.id);
                        $('#ddnEmailType'+ counter).val(email.emailType);
                        $('#txtEmailAddress'+ counter).val(email.email);
                    }
                }
                len = $('[id^='+strEmail+']').filter(':visible').length;
                for(let index=emails.length;index<len;index++){
                    hideDynamicDiv(strEmail+(index+1));
                }
            }

            let universities = data['universities'];
            if(universities && universities.length>0){
                for(let index=0,len=universities.length;index<len;index++){
                    let counter = index+1;
                    let university = universities[index];
                    if(university){
                        if(index > 0)
                            addEducationDiv();
                        $('#hdnUniversityId'+counter).val(university.universityId);
                        $('#ddnUniversityType'+ counter).val(university.universityType);
                        $('#txtUniversityName'+ counter).val(university.universityName);
                        $('#txtFromUniversityDate'+ counter).val(university.fromDate);
                        $('#txtToUniversityDate'+ counter).val(university.toDate);                    
                        if(university.toDate === null){
                            $('#chkCurrentUniveristy'+counter).prop('checked',true);
                            processToDate(document.getElementById('chkCurrentUniveristy'+counter),'txtToUniversityDate'+ counter);
                        }
                    }
                }
                len = $('[id^='+strUniversity+']').filter(':visible').length;
                for(let index=universities.length;index<len;index++){
                    hideDynamicDiv(strUniversity+(index+1));
                }
            }
            
            let researches = data['researches'];
            if(researches && researches.length>0){
                for(let index=0,len=researches.length;index<len;index++){
                    let counter = index+1;
                    let research = researches[index];
                    if(research){
                        if(!$('#divResearch_'+counter).is(':visible'))
                            addResearchDiv();
                        $('#hdnResearchId'+counter).val(research.researcheId);
                        $('#txtTopicName'+ counter).val(research.researchName);
                        $('#txtTopicDesc'+ counter).val(research.researchDesc);
                    }
                }

                len = $('[id^='+strResearch+']').filter(':visible').length;
                for(let index=researches.length;index<len;index++){
                    hideDynamicDiv(strResearch+(index+1));
                }
            }

            let employers = data['employers'];
            if(employers && employers.length>0){
                for(let index=0,len=employers.length;index<len;index++){
                    let counter = index+1;
                    let employer = employers[index];
                    if(employer){
                        if(!$('#divEmployer_'+counter).is(':visible'))
                            addEmployerDiv();
                        $('#hdnEmployerId'+counter).val(employer.employersId);
                        $('#txtEmployerName'+ counter).val(employer.employerName);
                        $('#txtFromEmployerDate'+ counter).val(employer.fromDate);
                        $('#txtToEmployerDate'+ counter).val(employer.toDate);                    
                        if(employer.toDate === null){
                            $('#chkCurrentEmployer'+counter).prop('checked',true);
                            processToDate(document.getElementById('chkCurrentEmployer'+counter),'txtToEmployerDate'+ counter);
                        }
                    }
                }
                len = $('[id^='+strEmployer+']').filter(':visible').length;
                for(let index=employers.length;index<len;index++){
                    hideDynamicDiv(strEmployer+(index+1));
                }
            }

            $('.dropdown-menu').selectmenu('refresh');
            $('#addUser').text('Update User');
            formAction = 'Update';
        }
    }
    catch(e){
        console.log(e);
    }
}

function getSpecificInfo(personId,attributeToGet){
    try{
        let serverData = {};
        serverData['personId'] = personId;
        serverData['attributeToGet'] = attributeToGet;
        makeServerCall("GetSpecificUserInfo",serverData,viewUserProcess);
    }
    catch(e){
        console.log(e);
    }
}

function generateUserTable(responseData){
    try{
        let outString = 'No user(s) found......';
        if(responseData){
            let serverResponce = JSON.parse(responseData);
            if(serverResponce){
                let jsonData = serverResponce.PersonData;
                if(jsonData){
                    let rowString = '';
                    for(let index = 0,len=jsonData.length;index<len;index++){
                        rowString+= '<tr>'
                                        + '<td>' + '#' + (index+1) + '</td>'
                                        + '<td>' + jsonData[index]['firstName'] + '</td>'
                                        + '<td>' + jsonData[index]['lastName']+ '</td>'
                                        + '<td> <a href="javascript:void(0);" class="summaryView" onclick="getSpecificInfo(\'' + jsonData[index]['personId'] + '\',\'Address\')">View Address(es)</a></td>'
                                        + '<td> <a href="javascript:void(0);" class="summaryView" onclick="getSpecificInfo(\'' + jsonData[index]['personId'] + '\',\'ContactNumber\')">View contact number(s)</a></td>'
                                        + '<td> <a href="javascript:void(0);" class="summaryView" onclick="getSpecificInfo(\'' + jsonData[index]['personId'] + '\',\'EmailAddress\')">View email id(s)</a></td>'
                                        + '<td> <a href="javascript:void(0);" class="summaryView" onclick="getSpecificInfo(\'' + jsonData[index]['personId'] + '\',\'University\')">View universities</a></td>'
                                        + '<td> <a href="javascript:void(0);" class="summaryView" onclick="getSpecificInfo(\'' + jsonData[index]['personId'] + '\',\'Research\')">View research(es)</a></td>'
                                        + '<td> <a href="javascript:void(0);" class="summaryView" onclick="getSpecificInfo(\'' + jsonData[index]['personId'] + '\',\'Employer\')">View employment(s)</a></td>'
                                        + '<td>'
                                            + '<a href="javascript:void(0);" class="glyphicon glyphicon-info-sign" onclick="viewUser(\'' + jsonData[index]['personId'] + '\')"></a>'
                                            + '<a href="javascript:void(0);" class="glyphicon glyphicon-edit" onclick="editUser(\'' + jsonData[index]['personId'] + '\')"></a>'
                                            + '<a href="javascript:void(0);" class="glyphicon glyphicon-trash" onclick="return confirm(\'Are you sure to delete data?\')?deleteUser(\'' + jsonData[index]['personId'] + '\'):false;"></a>'
                                        + '</td>'
                                    + '</tr>';            
                    }
                    let tableString = '<table class="table table-striped">'
                                    + '<thead>'
                                        + '<tr>'
                                        + '<th></th>'
                                        + '<th>First Name</th>'
                                        + '<th>Last Name</th>'
                                        + '<th>Addresses</th>'
                                        + '<th>Contact Numbers</th>'
                                        + '<th>Email Ids</th>'
                                        + '<th>Education History</th>'
                                        + '<th>Past Researches</th>'
                                        + '<th>Employment History</th>'
                                        + '<th>Action</th>'
                                        + '</tr>'
                                    + '</thead>'
                                    + '<tbody id="userData">'
                                        + rowString
                                    + '</tbody>'
                                    + '</table>';
                    outString = tableString;
                }
            }
        }
        $('#divTblAllUsers').html(outString);
    }
    catch(e){
        console.log(e);
    }
}

function displaySearch(){
    formAction = 'Search';
    $('#allUsers, #divAddUser').addClass('none');
    $('#divSearches').removeClass('none').addClass('show');
}

function initiateTableInformation(){
    $('#txtInColval').val('');
    $('input[type="radio"]').attr('checked',false);
    $('input[type="checkbox"]').attr('checked',false);
    $('#tblInNames input[type=radio]').on('click',function(){
        $('.colInNames').addClass('none').removeClass('show');
        let selection = $(this);
        let selectedId = selection.prop('id').replace('rd','tblIn') + 'ColNames';
        $('#divInColumns').removeClass('none').addClass('show');
        $('#' + selectedId).removeClass('none').addClass('show');
        $('#divTxtInColVal').removeClass('show').addClass('none');
    });
    $('.colRadios').on('click',function(){
        let selection = $(this);
        let tbleName = selection.prop('name').replace('tbl','');
        let columnType = selection.prop('id').replace('rd','').substring(0,1);
        let columnId = selection.prop('id').replace('rd','').replace(columnType,'');
        searchCriteria.inTbleId = tbleName;
        searchCriteria.inColId = columnId;
        $('#tblOutNames').removeClass('none').addClass('show');
        $('#divTxtInColVal').removeClass('none').addClass('show');
        
        /*generate drop downs from the json.
        * add a confirm button to finalize the input selection
        * copy paste input for output and convert radio to checkbox
        * allow selection upto two
        */
    });
    $('#tblOutNames input[type="checkbox"]').on('click',function(){
        //$('.colOutNames').addClass('none').removeClass('show');
        let selectedCount = $('#tblOutNames input:checked').length;
        
        if(selectedCount > 2){
            alert('Only two selections are allowed');
            $(this).attr('checked',false);
        }
        else{

            if(selectedCount <=0){
                $('#divOutColumns').removeClass('show').addClass('none');
                $('#divOutColumns input:checked').attr('checked',false);
                $('#divInButton').removeClass('show').addClass('none');
            }

            let selection = $(this);
            let selectedId = selection.prop('id').replace('chk','tblOut') + 'ColNames';

            if((selection).is(':checked')){
                $('#divOutColumns').removeClass('none').addClass('show');
                $('#' + selectedId).removeClass('none').addClass('show');
            }
            else{
                $('#' + selectedId).removeClass('show').addClass('none');
            }
        }
    });

    let previousOutTblName = '';
    $('.colCheckboxes').on('click',function(){
        let selectedCount = $('.colOutNames').find(':checked').length;
        if(selectedCount >2){
            alert('Only two selections are allowed');
            $(this).attr('checked',false);
        }
        else{
            let selection = $(this);
            let tbleName = selection.prop('name').replace('tbl','');
            let columnType = selection.prop('id').replace('chk','').substring(0,1);
            let columnId = selection.prop('id').replace('chk','').replace(columnType,'');
            let displayName = selection.next('p').html();
            if(selection.is(':checked')){
                
                if(tbleName !== previousOutTblName){
                    searchCriteria['outTblId'+selectedCount] = tbleName;
                    previousOutTblName = tbleName;
                }
                searchCriteria['outColId'+selectedCount] = columnId;
                searchCriteria['outColDisplayName'+selectedCount] = displayName;
            }
            else{
                
                let keys = Object.keys(searchCriteria);
                for(let index=0;index<keys.length;index++){
                    let key = keys[index];
                    if(searchCriteria[key] === columnId){
                        searchCriteria[key] = '';
                    }
                    if(searchCriteria[key] === displayName)
                        searchCriteria[key] = '';
                }
            }
            
            if(selectedCount > 0)
                $('#divInButton').removeClass('none').addClass('show');
            else
                $('#divInButton').removeClass('show').addClass('none');            
        }
    });
}

function doSeach(){
    if($('#txtInColval').val() === ''){
        alert('Please enter the value to compare in "Value to look for"');
    }
    else{
        searchCriteria.inColVal = $('#txtInColval').val();
        $('#viewData').html('Is the selection for search correct?');
        openDialog(searchCriteria,true);
    }
}

function displaySearchResults(responseData){
    
    if(responseData){
        let data = JSON.parse(responseData);
        if(searchCriteria.outColId2){
            let tableString = '<table class="table table-striped" style="margin-left:auto;margin-right:auto;text-align:left;"><tr><td>'+searchCriteria["outColDisplayName1"]+'</td><td>'+searchCriteria["outColDisplayName2"] + '</td>';
            for(let index = 0,len = data.length;index<len;index++){
                tableString += '<tr><td><h3>' + data[index][searchCriteria["outColDisplayName1"]] + '</h3></td><td><h3>' + data[index][searchCriteria["outColDisplayName2"]] + '</h3></td></tr>'; 
            }
            tableString += '</table>';
            $('#viewData').html(tableString);
            openDialog(null,false);
        }
        else{
            let tableString = '<table class="table table-striped" style="margin-left:auto;margin-right:auto;text-align:left;border-style:solid"><tr><td>' + searchCriteria["outColDisplayName1"]+'</td>';
            for(let index = 0,len = data.length;index<len;index++){
                tableString += '<tr><td><h3>' + data[index][searchCriteria["outColDisplayName1"]] + '</h3></td></tr>'; 
            }
            tableString += '</table>';
            $('#viewData').html(tableString);
            openDialog(null,false);
        }
    }
    else
        alert('No data found');
}
// let title = confirmation ? 'Record Confirmation' : 'Record Information';

//        let buttons = [];
//        let cancelButton = 
//        {
//            text: confirmation ? 'Cancel' : 'Close',
//            class: "btn btn-warning",
//            click: function() {
//                 $( this ).dialog( "close" );
//             }
//         }
//         buttons.push(cancelButton);  

//         let confirmButton =
//         {
//             text: 'Confirm',
//             class: "btn btn-success",
//             click: function() {
//                 $( this ).dialog( "close" );
//             }
//         }
//         if(confirmation)
//             buttons.push(confirmButton);

    //    let dialog = $('#viewData').dialog
    //    ({
    //        draggable:false,
    //        title:title,
    //        modal:true,
    //        buttons:buttons,
    //        open:function(event, ui){
    //            $('.ui-dialog-titlebar-close', ui.dialog|ui).hide();
    //        }
    //    })

//    let dialog = $( "#addForm" ).dialog
//     ({
//         autoOpen: false,
//         height: 400,
//         width: 350,
//         modal: true,
//         // buttons: {
//         //     "Create an account": addPerson,
//         //     Cancel: function() {
//         //     dialog.dialog( "close" );
//         //     }
//         // },
//         open:function(){
//             $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
//         },
//         close: function() {
//             form[ 0 ].reset();
//             //allFields.removeClass( "ui-state-error" );
//         }
//     });

//let form = dialog.find("form");