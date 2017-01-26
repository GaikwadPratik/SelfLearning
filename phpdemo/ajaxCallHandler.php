<?php
include('dbconfig.php');

echo $_POST["method"]();

function InsertNewRecord()
{
    if(isset($_POST['serverData']))
    {
        $rtnVal = 'success';
        $serverData = json_decode($_POST['serverData']);
        $personData = $serverData->person;
        if($personData)
        {
            $dataBase = new Database();
            $personId = $dataBase->InsertNewPerson($personData->firstName, $personData->lastName, $personData->sex);
            if($personId)
            {
                $npersonId = intval($personId);
                $addresses = $serverData->addresses;
                if($addresses)
                {
                    ProcessAddress($dataBase,$npersonId,$addresses);
                }
                $phoneNos = $serverData->phonenos;
                if($phoneNos)
                {
                    ProcessPhone($dataBase,$npersonId,$phoneNos);
                }

                $emails = $serverData->emails;
                if($emails)
                {
                    ProcessEmai($dataBase,$npersonId,$emails);
                }

                $universities = $serverData->universities;
                if($universities)
                {
                    ProcessUniversity($dataBase,$npersonId,$universities);
                }

                if(isset($serverData->researches))
                {
                    $researches = $serverData->researches;
                    ProcessResearch($dataBase,$npersonId,$researches);
                }

                if(isset($serverData->employers))
                {
                    $employers = $serverData->employers;
                    ProcessEmployer($dataBase,$npersonId,$employers);
                }
            }
        }
        return json_encode($rtnVal);
    }
}

function UpdateRecord()
{
    $status = 'Failed';
    if(isset($_POST['serverData']))
    {
        $serverData = json_decode($_POST['serverData']);
        $personData = $serverData->person;
        if($personData)
        {
            $npersonId = intval($personData->personId);
            $dataBase = new Database();
            $dataBase->UpdatePerson($npersonId, $personData->firstName, $personData->lastName, $personData->sex);

            $addresses = $serverData->addresses;
            if($addresses)
            {
                ProcessAddress($dataBase,$npersonId,$addresses);
            }
            
            $phoneNos = $serverData->phonenos;
            if($phoneNos)
            {
                ProcessPhone($dataBase,$npersonId,$phoneNos);
            }

            $emails = $serverData->emails;
            if($emails)
            {
                ProcessEmai($dataBase,$npersonId,$emails);
            }

            $universities = $serverData->universities;
            if($universities)
            {
                ProcessUniversity($dataBase,$npersonId,$universities);
            }

            if(isset($serverData->researches))
            {
                $researches = $serverData->researches;
                foreach ($researches as $key => $value) 
                {
                    ProcessResearch($dataBase,$npersonId,$researches);
                }
            }

            if(isset($serverData->employers))
            {
                $employers = $serverData->employers;
                ProcessEmployer($dataBase,$npersonId,$employers);
            }

            if(isset($serverData->ToBeDeleted))
            {
                $toBeDeleted = $serverData->ToBeDeleted;
                foreach ($toBeDeleted as $key => $value) 
                {
                    switch ($key) {
                        case 'Address':
                            foreach ($value as $key => $value) 
                            {
                                $dataBase->DeleteAddress($npersonId,$value);
                            }
                            break;
                        case 'ContactNumber':
                            foreach ($value as $key => $value) 
                            {
                                $dataBase->DeletePhone($npersonId,$value);
                            }
                            break;
                        case 'EmailAddress':
                            foreach ($value as $key => $value) 
                            {
                                $dataBase->DeleteEmail($npersonId,$value);
                            }
                            break;
                        case 'University':
                            foreach ($value as $key => $value) 
                            {
                                $dataBase->DeleteUniversity($npersonId,$value);
                            }
                            break;
                        case 'Research':
                            foreach ($value as $key => $value) 
                            {
                                $dataBase->DeleteResearch($npersonId,$value);
                            }
                            break;
                        case 'Employer':
                            foreach ($value as $key => $value) 
                            {
                                $dataBase->DeleteEmployer($npersonId,$value);
                            }
                            break;
                        default:
                            # code...
                            break;
                    }
                    
                }
            }
            $status = 'Success';
        }
    }
    return $status;
}

function GetUser()
{
    $returnVal = new stdClass();
    $dataBase = new Database();
    $serverData = json_decode($_POST['serverData']);
    $personId = -1;
    if(isset($serverData->personId))
    {
        $personId = $serverData->personId;
        $personId = (int)$personId;
    
        $personInfo = $dataBase->GetPersons($personId);
        if($personInfo)
        {
            foreach ($personInfo as $key => $person) 
            {
                $personData = new stdClass();
                $personData->personId = $personId;
                $personData->firstName = $person['firstName'];
                $personData->lastName = $person['lastName'];
                $personData->sex = $person['sex'];
                $returnVal->person = $personData;

                $addressData = GetAddresses($dataBase,$personId);
                if($addressData && count($addressData)>0)
                    $returnVal->addresses = $addressData;

                $phoneData = GetPhoneNumbers($dataBase,$personId);
                if($phoneData && count($phoneData)>0)
                    $returnVal->phonenos = $phoneData;

                $emailData = GetEmails($dataBase,$personId);
                if($emailData && count($emailData)>0)
                    $returnVal->emails = $emailData;
                
                $universityData = GetUniversites($dataBase,$personId);
                if($universityData && count($universityData)>0)
                    $returnVal->universities = $universityData;

                $researchData = GetResearches($dataBase,$personId);
                if($researchData && count($researchData)>0)
                    $returnVal->researches = $researchData;

                $employerData = GetEmployers($dataBase,$personId);
                if($employerData && count($employerData)>0)
                    $returnVal->employers = $employerData;                
            }
        }
    }
    
    return json_encode($returnVal);
}

function DeleteUser()
{
    $dataBase = new Database();
    $serverData = json_decode($_POST['serverData']);
    $personId = -1;
    if(isset($serverData->personId))
    {
        $personId = $serverData->personId;
        $personId = (int)$personId;
        $deleted = $dataBase->DeleteUser($personId);
    }
}

function ProcessPhone($dataBase,$npersonId,$phoneNos)
{
    foreach($phoneNos as $key => $value)
    {
        $phoneTypeId = $value->contactType;
        if(isset($value->contactTypeName))
        {
            $phoneTypeId = $dataBase->AddPhoneType($value->contactTypeName);
        }
        $nphoneTypeId = (int)$phoneTypeId;
        $phoneNo = $value->number;
        if(!empty($value->phonenoId))
            $dataBase->UpdatePhone($npersonId,$nphoneTypeId,$phoneNo,intval($value->phonenoId));
        else
            $dataBase->InsertNewPhone($npersonId,$nphoneTypeId,$phoneNo);
    }
}

function ProcessAddress($dataBase,$npersonId,$addresses)
{
    foreach($addresses as $key => $value)
    {
        $addressTypeId = $value->addressType;
        if(isset($value->addressTypeName))
        {
            $addressTypeId = $dataBase->AddAddressType($value->addressTypeName);
        }
        $naddressTypeId = (int)$addressTypeId;
        $firstLine = $value->firstLine;
        $lastLine = $value->secondLine;
        $city = $value->city;
        $state = $value->state;
        $zip = $value->zip;
        if(!empty($value->addressId))
            $dataBase->UpdateAddress($npersonId,$naddressTypeId,$firstLine,$lastLine,$city,$state,$zip,intval($value->addressId));
        else
            $dataBase->InsertNewAddress($npersonId,$naddressTypeId,$firstLine,$lastLine,$city,$state,$zip);
    }
}

function ProcessEmai($dataBase,$npersonId,$emails)
{
    foreach($emails as $key => $value)
    {
        $emailTypeId = $value->emailType;
        if(isset($value->emailTypeName))
        {
            $emailTypeId = $dataBase->AddEmailType($value->emailTypeName);
        }
        $nemailTypeId = (int)$emailTypeId;
        $email = $value->email;
        if(!empty($value->id))
            $dataBase->UpdateEmail($npersonId,$nemailTypeId,$email,intval($value->id));
        else
            $dataBase->InsertNewEmail($email, $npersonId,$nemailTypeId);
    }
}

function ProcessUniversity($dataBase,$npersonId,$universities)
{
    foreach($universities as $key => $value)
    {
        $universityTypeId = $value->universityType;
        if(isset($value->universityTypeName))
        {
                $universityTypeId = $dataBase->AddUniversityType($value->universityTypeName);
        }
        $nuniversityTypeId = (int)$universityTypeId;
        $universityName = $value->universityName;
        $fromDate = date("Y-m-d H:i:s",strtotime($value->fromDate));
        
        if(!empty($value->toDate))
            $toDate = "'".date("Y-m-d H:i:s",strtotime($value->toDate))."'";
        else
            $toDate = 'NULL';
        if(!empty($value->universityId))
            $dataBase->UpdateUniversity($npersonId,$nuniversityTypeId,$universityName,$fromDate,$toDate,intval($value->universityId));
        else
            $dataBase->InsertNewUniversity($npersonId,$nuniversityTypeId,$universityName,$fromDate,$toDate);
    }
}

function ProcessResearch($dataBase,$npersonId,$researches)
{
    foreach ($researches as $key => $value) 
    {
        if(!empty($value->researcheId))
            $dataBase->UpdateResearch($npersonId,$value->researchName,$value->researchDesc,intval($value->researcheId));
        else
            $dataBase->InsertNewResearch($npersonId,$value->researchName,$value->researchDesc);
    }
}

function ProcessEmployer($dataBase,$npersonId,$employers)
{
    foreach($employers as $key => $value)
    {                        
        $employerName = $value->employerName;
        $fromDate = date("Y-m-d H:i:s",strtotime($value->fromDate));
        if(!empty($value->toDate))
            $toDate = "'".date("Y-m-d H:i:s",strtotime($value->toDate))."'";
        else
            $toDate = 'NULL';
        if(!empty($value->employersId))
            $dataBase->UpdateEmployer($npersonId,$employerName,$fromDate,$toDate,intval($value->employersId));
        else
            $dataBase->InsertNewEmployer($npersonId,$employerName,$fromDate,$toDate);
    }
}

function GetAddresses($dataBase,$personId)
{
    $addressData = [];    
    $addresses = $dataBase->GetAddresses($personId);
    if($addresses)
    {
        foreach ($addresses as $key => $address) 
        {
            # addresseId,personId,addressType,firstLine,lastLine,city,state,zip
            $add = new stdClass();
            $add->addressId = $address['addresseId'];
            $add->addressType = $address['addressType'];
            $add->firstLine = $address['firstLine'];
            $add->secondLine = $address['lastLine'];
            $add->city = $address['city'];
            $add->state = $address['state'];
            $add->zip = $address['zip'];
            array_push($addressData,$add);
        }
    }
    return $addressData;
}

function GetPhoneNumbers($dataBase,$personId)
{
    $phoneData = [];
    $phones = $dataBase->GetPhones($personId);
    if($phones)
    {
        foreach ($phones as $key => $phone) 
        {
            # phonenoId,personId,phoneTypeId,phoneno
            $add = new stdClass();
            $add->phonenoId = $phone['phonenoId'];
            $add->contactType = $phone['phoneTypeId'];
            $add->number = $phone['phoneno'];
            array_push($phoneData, $add);
        }        
    }
    return $phoneData;
}

function GetEmails($dataBase,$personId)
{
    # code...
    $emailData = [];
    $emails = $dataBase->GetEmails($personId);
    if($emails)
    {
        foreach ($emails as $key => $email) 
        {
            # id,personId,emailtypeid,emailId
            $add = new stdClass();
            $add->id = $email['id'];
            $add->emailType = $email['emailtypeid'];
            $add->email= $email['emailId'];
            array_push($emailData, $add);
        }   
    }
    return $emailData;
}

function GetUniversites($dataBase,$personId)
{
    # code...
    $universityData = [];
    $universities = $dataBase->GetUniversities($personId);
    if($universities)
    {
        foreach ($universities as $key => $value) 
        {
            # universityId,personId,universityType,universityName,fromDate,toDate
            $add = new stdClass();
            $add->universityType = $value['universityType'];
            $add->universityId = $value['universityId'];
            $add->universityName = $value['universityName'];
            $add->fromDate = date("m/d/Y",strtotime($value['fromDate']));
            $add->toDate = ($value['toDate'])?date("m/d/Y",strtotime($value['toDate'])):null;
            array_push($universityData,$add);
        }        
    }

    return $universityData;
}

function GetResearches($dataBase,$personId)
{
    # code...
    $researchData = [];
    $researches = $dataBase->GetResearches($personId);
    if($researches)
    {
        foreach ($researches as $key => $value) 
        {
            # researcheId,personId,researcheName,researchesDesc
            $add = new stdClass();
            $add->researcheId = $value['researcheId'];
            $add->researchName = $value['researcheName'];
            $add->researchDesc= $value['researchesDesc'];
            array_push($researchData, $add);
        }                    
    }
    return $researchData;
}

function GetEmployers($dataBase,$personId)
{
    # code...
    $employerData = [];
    $employers = $dataBase->GetEmployers($personId);
    if($employers)
    {
        foreach ($employers as $key => $value) 
        {
            # employersId,personId,employerName,fromDate,toDate
            $add = new stdClass();
            $add->employersId = $value['employersId'];
            $add->employerName = $value['employerName'];                        
            $add->fromDate = date("m/d/Y",strtotime($value['fromDate']));
            $add->toDate = ($value['toDate']) ? date("m/d/Y",strtotime($value['toDate'])):null;
            array_push($employerData,$add);
        }        
    }
    return $employerData;
}

function GetSpecificUserInfo()
{
    $returnVal = new stdClass();
    $dataBase = new Database();
    $serverData = json_decode($_POST['serverData']);
    $personId = -1;
    if(isset($serverData->personId))
    {
        $personId = $serverData->personId;
        $personId = (int)$personId;
        switch ($serverData->attributeToGet) 
        {
            case 'Address':
                # code...
                $addressData = GetAddresses($dataBase,$personId);
                if($addressData && count($addressData)>0)
                    $returnVal->addresses = $addressData;
                break;
            
            case 'ContactNumber':
                # code...
                $phoneData = GetPhoneNumbers($dataBase,$personId);
                if($phoneData && count($phoneData)>0)
                    $returnVal->phonenos = $phoneData;
                break;
            
            case 'EmailAddress':
                # code...
                $emailData = GetEmails($dataBase,$personId);
                if($emailData && count($emailData)>0)
                    $returnVal->emails = $emailData;
                break;
            
            case 'University':
                # code...
                $universityData = GetUniversites($dataBase,$personId);
                if($universityData && count($universityData)>0)
                    $returnVal->universities = $universityData;
                break;
            
            case 'Research':
                # code...
                $researchData = GetResearches($dataBase,$personId);
                if($researchData && count($researchData)>0)
                    $returnVal->researches = $researchData;
                break;
            
            case 'Employer':
                # code...
                $employerData = GetEmployers($dataBase,$personId);
                if($employerData && count($employerData)>0)
                    $returnVal->employers = $employerData;    
                break;
            default:
                # code...
                break;
        }
    }
    return json_encode($returnVal);
}

function InitialCall()
{
    # code...
    $returnVal = new stdClass();
    $dataBase = new Database();
    $allPerson = $dataBase->GetPersons();
    if($allPerson)
        $returnVal->PersonData = $allPerson;
    $tableData = GetTableInformation($dataBase);
    // if($tableData)
    //     $returnVal->TableData = $tableData;
    return json_encode($returnVal);
}

function GetTableInformation($dataBase)
{
    $allTables = $dataBase->GetTableInformation();
    $rtnTableArr = [];
    $tableArr = [];
    $currTableName = '';
    //$tablNames = [];
    foreach ($allTables as $key => $value) 
    {
        # code...
        $tableChanged = false;
        if($currTableName !== $value['tblName'])
        {
            $currTableName = $value['tblName'];
            //array_push(tablNames,$currTableName);
            $tableChanged = true;
            $tableArr = [];
        }
        if($tableChanged && !array_key_exists($currTableName,$rtnTableArr))
        {
            array_push($tableArr,$value);
            $rtnTableArr[$currTableName] = $tableArr;
        }
        else
        {
            $oldTableArr = $rtnTableArr[$currTableName];
            array_push($oldTableArr,$value);
            $rtnTableArr[$currTableName] = $oldTableArr;
        }
    }
    //$rtnTableArr['TableNames'] = $tablNames;
    return $rtnTableArr;
}

function DynamicSearch()
{
    # code...
     if(isset($_POST['serverData']))
    {
        $serverData = json_decode($_POST['serverData']);
        if($serverData)
        {
            $dataBase = new Database();
            $returnVal = $dataBase->DynamicSearch($serverData);
            if($returnVal)
                return json_encode($returnVal);
        }
    }
}
?>