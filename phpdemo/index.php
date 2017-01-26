    <?php
        include('header.html');
        include('dbconfig.php');
    ?>
  
    <div class="container mainDiv">
        <div class="row">
            <div class="panel panel-default users-content">
                <div id="allUsers" class="panel-default">
                    <div class="panel-heading" id="divAddUser">
                        <h2>Users
                            <a href="javascript:void(0);" class="glyphicon glyphicon-plus" id="addLink" onclick="showAddUser(true);"></a>
                            <a href="javascript:void(0);" class="glyphicon glyphicon-search" onclick="displaySearch();"></a>
                        </h2>
                    </div>
                    <div class="panel-body" id="divTblAllUsers">
                        
                    </div>
                </div>
                <div id="addForm" class="panel-default none">
                    <!--<input type="hidden" id="hdnFormAction" name="hdnFormAction" value="Add" />-->
                    <div class="panel-heading"><h3>Add Person information</h3></div>
                    <div class="panel-body formData">
                        <form class="form" id="userForm">
                            <div><h3>Personal Information</h3></div>                            
                            <div class="form-group input-group">
                                <input type="hidden" name="hdnPersonId" id="hdnPersonId" value=""/>
                                <label class="input-group-addon">First Name: </label>
                                <input type="text" class="form-control" name="txtFirstName" id="txtFirstName" placeholder="Please enter your first name"/>
                            </div>
                            <div class="form-group input-group">
                                <label class="input-group-addon">Last Name: </label>
                                <input type="text" class="form-control" name="txtLastName" id="txtLastName" placeholder="Please enter your last name."/>
                            </div>
                            <div class="form-group input-group">
                                <label class="input-group-addon">Sex: </label>
                                <select name="ddnSex" id="ddnSex" class="dropdown-menu">
                                    <option value="">Please select</option>
                                    <option value="M">Male</option>
                                    <option value="F">Female</option>
                                </select>
                            </div>
                            <br />
                            <?php
                                $database = new Database();
                                $jsonAddressTypes = $database->GetAddressTypes();
                                $jsonUniversityType = $database->GetUniversityTypes();
                                $jsonPhoneType = $database->GetPhoneTypes();
                                $jsonEmailType = $database->GetEmailTypes();
                            ?>
                                                            
                            <div><h3>Address</h3></div>
                            <div id="divAddresses"></div>

                            <div><h3>Contact numbers</h3></div>
                            <div id="divContactNumberss"></div>

                            <div><h3>Email address</h3></div>
                            <div id="divEmailAddresses"></div>

                            <div><h3>Education</h3></div>
                            <div id="divUniversities"></div>
                            
                            <a href="javascript:void(0);" class="glyphicon glyphicon-plus" id="addResearch" onclick="addResearchDiv();">Add Research</a>
                            <div><h3>Reseach Topics</h3></div>
                            <div id="divResearches"></div>

                            <a href="javascript:void(0);" class="glyphicon glyphicon-plus" id="addEmployer" onclick="addEmployerDiv();">Add Employer</a>
                            <div><h3>Work Experience</h3></div>
                            <div id="divEmployers"></div>

                            <a href="javascript:void(0);" class="btn btn-success" id="addUser" onclick="addPerson();">Add User</a>
                            <a href="javascript:void(0);" class="btn btn-danger" id="btnReset" onclick="clearUserForm('btnReset');">Reset</a>
                            <a href="javascript:void(0);" class="btn btn-warning" id="btnCancel" onclick="showAddUser(false);">Cancel</a>
                        </form>
                    </div>
                </div>
                <div id="viewData" class="modal-dialog none">
                </div>
                <div id="divSearches" class="none">
                    <div class="panel-heading" class="none"><h2>Search Criterias</h2></div>
                    <div class="panel-body">                        
                        <div id="tblInformation">
                            <div class="radio" id="tblInNames">
                                <h2>Input tables</h2>
                                <label><input type="radio" name="tblName" id="rdPerson">Person</label>
                                <label><input type="radio" name="tblName" id="rdAddress">Address</label>
                                <label><input type="radio" name="tblName" id="rdPhone">Phone</label>
                                <label><input type="radio" name="tblName" id="rdEmail">Email</label>
                                <label><input type="radio" name="tblName" id="rdUniversity">University</label>
                                <label><input type="radio" name="tblName" id="rdResearch">Research</label>
                                <label><input type="radio" name="tblName" id="rdEmployer">Employment</label>
                            </div>
                            <div id="divInColumns" class="none">
                                <h2> Input column names</h2>
                                <div class="radio none colInNames" id="tblInPersonColNames">
                                    <label><input class="colRadios" type="radio" name="tblperson" id="rdsfirstName">First Name</label></br>
                                    <label><input class="colRadios" type="radio" name="tblperson" id="rdslastName">Last Name</label>
                                </div>
                                <div class="radio none colInNames" id="tblInAddressColNames">
                                    <!--<label><input class="colRadios" type="radio" name="tbladdresses" id="rdnaddressType">Address Type</label></br>-->
                                    <label><input class="colRadios" type="radio" name="tbladdresses" id="rdsfirstLine">Line 1</label></br>
                                    <label><input class="colRadios" type="radio" name="tbladdresses" id="rdslastLine">Line 2</label></br>
                                    <label><input class="colRadios" type="radio" name="tbladdresses" id="rdscity">City</label></br>
                                    <label><input class="colRadios" type="radio" name="tbladdresses" id="rdsstate">State</label></br>
                                    <label><input class="colRadios" type="radio" name="tbladdresses" id="rdszip">Zip</label>
                                </div>
                                <div class="radio none colInNames" id="tblInPhoneColNames">
                                    <!--<label><input class="colRadios" type="radio" name="tblphonenos" id="rdnphoneTypeId">Phone Type</label></br>-->
                                    <label><input class="colRadios" type="radio" name="tblphonenos" id="rdsphoneno">Phone Number</label></br>
                                </div>
                                <div class="radio none colInNames" id="tblInEmailColNames">
                                    <!--<label><input class="colRadios" type="radio" name="tblemails" id="rdnemailtypeid">Email Type</label></br>-->
                                    <label><input class="colRadios" type="radio" name="tblemails" id="rdsemailId">Email Id</label></br>
                                </div>
                                <div class="radio none colInNames" id="tblInUniversityColNames">
                                    <!--<label><input class="colRadios" type="radio" name="tbluniversities" id="rdnuniversityType">University Type</label></br>-->
                                    <label><input class="colRadios" type="radio" name="tbluniversities" id="rdsuniversityName">University Name</label></br>
                                </div>
                                <div class="radio none colInNames" id="tblInResearchColNames">
                                    <label><input class="colRadios" type="radio" name="tblresearches" id="rdsresearcheName">Research Name</label></br>
                                    <label><input class="colRadios" type="radio" name="tblresearches" id="rdsresearchesDesc">Research Description</label></br>
                                </div>
                                <div class="radio none colInNames" id="tblInEmployerColNames">
                                    <label><input class="colRadios" type="radio" name="tblemployer" id="rdsemployerName">Employer Name</label>
                                </div>
                                <div class="textbox none" id="divTxtInColVal">
                                    Value to look for: <input type="textbox" id="txtInColval" name="txtInColVal" value="" placeholder="type in value to match with selected column" />
                                </div>
                            </div>
                            <div class="checkbox none" id="tblOutNames">
                                <h2>Out tables</h2>
                                <label><input type="checkbox" name="tblName" id="chkPerson">Person</label>
                                <label><input type="checkbox" name="tblName" id="chkAddress">Address</label>
                                <label><input type="checkbox" name="tblName" id="chkPhone">Phone</label>
                                <label><input type="checkbox" name="tblName" id="chkEmail">Email</label>
                                <label><input type="checkbox" name="tblName" id="chkUniversity">University</label>
                                <label><input type="checkbox" name="tblName" id="chkResearch">Research</label>
                                <label><input type="checkbox" name="tblName" id="chkEmployer">Employment</label>
                            </div>
                            <div id="divOutColumns" class="none">
                                <h2> Out column names</h2>
                                <div class="checkbox none colOutNames" id="tblOutPersonColNames">
                                    <label><input class="colCheckboxes" type="checkbox" name="tblperson" id="chksfirstName"><p>First Name</p></label></br>
                                    <label><input class="colCheckboxes" type="checkbox" name="tblperson" id="chkslastName"><p>Last Name</p></label>
                                </div>
                                <div class="checkbox none colOutNames" id="tblOutAddressColNames">
                                    <!--<label><input class="colCheckboxes" type="checkbox" name="tbladdresses" id="chknaddressType">Address Type</label></br>-->
                                    <label><input class="colCheckboxes" type="checkbox" name="tbladdresses" id="chksfirstLine"><p>Line 1</p></label></br>
                                    <label><input class="colCheckboxes" type="checkbox" name="tbladdresses" id="chkslastLine"><p>Line 2</p></label></br>
                                    <label><input class="colCheckboxes" type="checkbox" name="tbladdresses" id="chkscity"><p>City</p></label></br>
                                    <label><input class="colCheckboxes" type="checkbox" name="tbladdresses" id="chksstate"><p>State</p></label></br>
                                    <label><input class="colCheckboxes" type="checkbox" name="tbladdresses" id="chkszip"><p>Zip</p></label>
                                </div>
                                <div class="checkbox none colOutNames" id="tblOutPhoneColNames">
                                    <!--<label><input class="colCheckboxes" type="checkbox" name="tblphonenos" id="chknphoneTypeId">Phone Type</label></br>-->
                                    <label><input class="colCheckboxes" type="checkbox" name="tblphonenos" id="chksphoneno"><p>Phone Number</p></label></br>
                                </div>
                                <div class="checkbox none colOutNames" id="tblOutEmailColNames">
                                    <!--<label><input class="colCheckboxes" type="checkbox" name="tblemails" id="chknemailtypeid">Email Type</label></br>-->
                                    <label><input class="colCheckboxes" type="checkbox" name="tblemails" id="chksemailId"><p>Email Id</p></label></br>
                                </div>
                                <div class="checkbox none colOutNames" id="tblOutUniversityColNames">
                                    <!--<label><input class="colCheckboxes" type="checkbox" name="tbluniversities" id="rdnuniversityType">University Type</label></br>-->
                                    <label><input class="colCheckboxes" type="checkbox" name="tbluniversities" id="chksuniversityName"><p>University Name</p></label></br>
                                </div>
                                <div class="checkbox none colOutNames" id="tblOutResearchColNames">
                                    <!--<label><input class="colCheckboxes" type="checkbox" name="tblresearches" id="rdsresearcheName">Research Name</label></br>-->
                                    <label><input class="colCheckboxes" type="checkbox" name="tblresearches" id="chksresearchesDesc"><p>Research Description</p></label></br>
                                </div>
                                <div class="checkbox none colOutNames" id="tblOutEmployerColNames">
                                    <label><input class="colCheckboxes" type="checkbox" name="tblemployer" id="rdsemployerName">Employer Name</label>
                                </div>
                            </div>
                            <div id="divInButton" class="none">
                                <a href="javascript:void(0);" class="btn btn-success" id="btntblBtnConfirm" onclick="doSeach();">Confirm selection</a>                                
                            </div>
                        </div>
                        <div id="divSearchResults" class="none">
                        </div>
                        <a href="javascript:void(0);" class="btn btn-warning" id="btntblBtnCancel" onclick="showAddUser(false);">Cancel</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">        
        let jsonAddressTypes, jsonPhoneType, jsonEmailType, jsonUniversityType, jsonTableInformation = null;
        $(document).ready(function(){            
            jsonAddressTypes = <?php echo json_encode($jsonAddressTypes) ?>;
            jsonUniversityType = <?php echo json_encode($jsonUniversityType) ?>;
            jsonEmailType = <?php echo json_encode($jsonEmailType) ?>;
            jsonPhoneType  = <?php echo json_encode($jsonPhoneType) ?>;            
        });
    </script>
    <script src="script.js"></script>
    <?php include('footer.html'); ?>
