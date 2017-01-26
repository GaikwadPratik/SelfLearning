<?php
ini_set('display_errors', 'On');
error_reporting(E_ALL | E_STRICT);
class Database
{
    private $dbHost     = "localhost";
	private $dbUsername = "root";
	private $dbPassword = "toor";
	private $dbName     = "demo_schema";

    public function __construct()
    {
        $conn = new mysqli($this->dbHost, $this->dbUsername, $this->dbPassword, $this->dbName);
		if($conn->connect_error)
        {
            die("Failed to connect with MySQL: " . $conn->connect_error);
		}
        else
        {
            $this->db = $conn;
		}
    }    

    public function GetAddressTypes()
    {
        $selectQuery = "SELECT addressTypeId,addressTypeName FROM addresstypes WHERE !isDeleted";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {            
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        else
        {
            echo "Unable to retrieve addresstypes data";
        }
        return NULL;
    }

    public function GetUniversityTypes()
    {
        $selectQuery = "SELECT universitytypeId,universitytypeName FROM universitytype WHERE !isDeleted";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {            
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        else
        {
            echo "Unable to retrieve universitytype data";
        }
        return NULL;
    }

    public function GetPhoneTypes()
    {
        $selectQuery = "SELECT phoneTypesId,phoneTypesName FROM phonetypes WHERE !isDeleted";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {            
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        else
        {
            echo "Unable to retrieve phonetypes data";
        }
        return NULL;
    }

    public function GetEmailTypes()
    {
        $selectQuery = "SELECT emailTypeid,emailTypeName FROM emailtypes WHERE !isDeleted";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        else
        {
            echo "Unable to retrieve emailtypes data";
        }
        return NULL;
    }

    public function AddAddressType($addressTypeName)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewAddressType('".$addressTypeName."',@outMessage);";
        $selectMessageQuery = "SELECT @outMessage;";
        $lastInsertedQuery = "SELECT LAST_INSERT_ID();";

        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $outMessage = $this->db->query($selectMessageQuery);
            if($outMessage)
            {
                $insertMessage = $outMessage->fetch_object();
                if($insertMessage == "SUCCESS")
                {
                    $result = $this->db->query($lastInsertedQuery);                    
                    if($result)
                    {
                        $returnVal = $result->fetch_object();
                    }
                }
                else
                {
                    $selectIdQuery = "SELECT addressTypeId FROM addresstypes WHERE LOWER(addressTypeName) = LOWER('".$addressTypeName."') && isDeleted = 0 LIMIT 1";
                    $idResult = $this->db->query($selectIdQuery);
                    if($idResult->num_rows > 0)
                    {
                        while($row = $idResult->fetch_assoc()){
                            $returnVal = $row['addressTypeId'];
                        }
                    }
                }
            }
        }

        return $returnVal;
    }

    public function AddUniversityType($universityTypeName)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewUniversityType('".$universityTypeName."',@outMessage);";
        $selectMessageQuery = "SELECT @outMessage;";
        $lastInsertedQuery = "SELECT LAST_INSERT_ID();";

        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $outMessage = $this->db->query($selectMessageQuery);
            if($outMessage)
            {
                $insertMessage = $outMessage->fetch_object();
                if($insertMessage == "SUCCESS")
                {
                    $result = $this->db->query($lastInsertedQuery);                    
                    if($result)
                    {
                        $returnVal = $result->fetch_object();
                    }
                }
                else
                {
                    $selectIdQuery = "SELECT universitytypeId FROM universitytype WHERE LOWER(universitytypeName) = LOWER('".$universityTypeName."') && isDeleted = 0 LIMIT 1";
                    $idResult = $this->db->query($selectIdQuery);
                    if($idResult->num_rows > 0)
                    {
                        while($row = $idResult->fetch_assoc()){
                            $returnVal = $row['universitytypeId'];
                        }
                    }
                }
            }
        }
        return $returnVal;
    }

    public function AddPhoneType($phoneTypeName)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InserNewPhoneType('".$phoneTypeName."',@outMessage);";
        $selectMessageQuery = "SELECT @outMessage;";
        $lastInsertedQuery = "SELECT LAST_INSERT_ID();";

        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $outMessage = $this->db->query($selectMessageQuery);
            if($outMessage)
            {
                $insertMessage = $outMessage->fetch_object();
                if($insertMessage == "SUCCESS")
                {
                    $result = $this->db->query($lastInsertedQuery);                    
                    if($result)
                    {
                        $returnVal = $result->fetch_object();
                    }
                }
                else
                {
                    $selectIdQuery = "SELECT phoneTypesId FROM phonetypes WHERE LOWER(phoneTypesName) = LOWER('".$phoneTypeName."') && isDeleted = 0 LIMIT 1";
                    $idResult = $this->db->query($selectIdQuery);
                    if($idResult->num_rows > 0)
                    {
                        while($row = $idResult->fetch_assoc()){
                            $returnVal = $row['phoneTypesId'];
                        }
                    }
                }
            }
        }
        return $returnVal;
    }

    public function AddEmailType($emailTypeName)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewEmailType('".$emailTypeName."',@outMessage);";
        $selectMessageQuery = "SELECT @outMessage;";
        $lastInsertedQuery = "SELECT LAST_INSERT_ID();";

        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $outMessage = $this->db->query($selectMessageQuery);
            if($outMessage)
            {
                $insertMessage = $outMessage->fetch_object();
                if($insertMessage == "SUCCESS")
                {
                    $result = $this->db->query($lastInsertedQuery);                    
                    if($result)
                    {
                        $returnVal = $result->fetch_object();
                    }
                }
                else
                {
                    $selectIdQuery = "SELECT emailTypeid FROM emailtypes WHERE LOWER(emailTypeName) = LOWER('".$emailTypeName."') && isDeleted = 0 LIMIT 1";
                    $idResult = $this->db->query($selectIdQuery);
                    if($idResult->num_rows > 0)
                    {
                        while($row = $idResult->fetch_assoc()){
                            $returnVal = $row['emailTypeid'];
                        }
                    }
                }
            }
        }
        return $returnVal;
    }
    
    public function InsertNewPerson($firstName,$lastName,$sex)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewPerson('".$firstName."','".$lastName."','".$sex."',@personId);";
        $selectPersonIdQuery = "SELECT @personId";
        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $result = $this->db->query($selectPersonIdQuery);
            if($result)
            {
                while($row = $result->fetch_assoc())
                    $returnVal = $row['@personId'];
            }
        }
        return $returnVal;
    }

    public function InsertNewAddress($personId,$addressType,$firstLine,$lastLine,$city,$state,$zip)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewAddress(".$personId.",".$addressType.",'".$firstLine."','".$lastLine."','".$city."','".$state."','".$zip."',@personId);";
        $selectPersonIdQuery = "SELECT @personId";
        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $result = $this->db->query($selectPersonIdQuery);
            if($result)
            {
                while($row = $result->fetch_assoc())
                    $returnVal = $row['@personId'];
            }
        }
        return $returnVal;
    }

    public function InsertNewPhone($personId,$phoneType,$phoneno)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewPhoneno(".$personId.",".$phoneType.",'".$phoneno."',@personId);";
        $selectPersonIdQuery = "SELECT @personId";
        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $result = $this->db->query($selectPersonIdQuery);
            if($result)
            {
                while($row = $result->fetch_assoc())
                    $returnVal = $row['@personId'];
            }
        }
        return $returnVal;
    }

    public function InsertNewEmail($email,$personId,$emailIdType)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewEmail('".$email."',".$personId.",".$emailIdType.",@personId);";
        $selectPersonIdQuery = "SELECT @personId";
        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $result = $this->db->query($selectPersonIdQuery);
            if($result)
            {
                while($row = $result->fetch_assoc())
                    $returnVal = $row['@personId'];
            }
        }
        return $returnVal;
    }

    public function InsertNewUniversity($personId,$universityType,$universityName,$fromDate,$toDate)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewUniversity(".$personId.",".$universityType.",'".$universityName."','".$fromDate."',".$toDate.",@personId);";
        $selectPersonIdQuery = "SELECT @personId";
        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $result = $this->db->query($selectPersonIdQuery);
            if($result)
            {
                while($row = $result->fetch_assoc())
                    $returnVal = $row['@personId'];
            }
        }
        return $returnVal;
    }

    public function InsertNewResearch($personId,$reseachName,$reseachDescription)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewResearch(".$personId.",'".$reseachName."','".$reseachDescription."',@personId);";
        $selectPersonIdQuery = "SELECT @personId";
        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $result = $this->db->query($selectPersonIdQuery);
            if($result)
            {
                while($row = $result->fetch_assoc())
                    $returnVal = $row['@personId'];
            }
        }
        return $returnVal;
    }

    public function InsertNewEmployer($personId,$employerName,$fromDate,$toDate)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_InsertNewEmployer(".$personId.",'".$employerName."','".$fromDate."',".$toDate.",@personId);";
        $selectPersonIdQuery = "SELECT @personId";
        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $result = $this->db->query($selectPersonIdQuery);
            if($result)
            {
                while($row = $result->fetch_assoc())
                    $returnVal = $row['@personId'];
            }
        }
        return $returnVal;
    }

    public function GetPersons($personId=-1)
    {
        $selectQuery = "SELECT personId,firstName,lastName,sex FROM person WHERE !isDeleted";
        if($personId !== -1)
            $selectQuery .= " && personId = ".$personId;
        $selectQuery.= ";";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        return NULL;
    }

    public function GetAddresses($personId=-1,$addressId=-1,$addressType=-1)
    {
        $selectQuery = "SELECT addresseId,personId,addressType,firstLine,lastLine,city,state,zip FROM addresses WHERE !isDeleted";
        if($personId !== -1)
            $selectQuery .= " && personId = ".$personId;
        if($addressId !== -1)
            $selectQuery .= " && addressId = ".$addressId;
        if($addressType !== -1)
            $selectQuery .= " && addressType = ".$addressType;
        $selectQuery.= ";";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        return NULL;
    }

    public function GetPhones($personId=-1,$phonenoId=-1,$phoneTypeId=-1)
    {
        $selectQuery = "SELECT phonenoId,personId,phoneTypeId,phoneno FROM phonenos WHERE !isDeleted";
        if($personId !== -1)
            $selectQuery .= " && personId = ".$personId;
        if($phonenoId !== -1)
            $selectQuery .= " && phonenoId = ".$phonenoId;
        if($phoneTypeId !== -1)
            $selectQuery .= " && phoneTypeId = ".$phoneTypeId;
        $selectQuery.= ";";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        return NULL;
    }

    public function GetEmails($personId=-1,$id=-1,$emailTypeId=-1,$email='')
    {
        $selectQuery = "SELECT id,personId,emailtypeid,emailId FROM emails WHERE !isDeleted";
        if($personId !== -1)
            $selectQuery .= " && personId = ".$personId;
        if($id !== -1)
            $selectQuery .= " && id = ".$id;
        if($emailTypeId !== -1)
            $selectQuery .= " && emailTypeId = ".$emailTypeId;
        if($email !== '')
            $selectQuery .= " && emailId = '".$email."'";
        $selectQuery.= ";";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        return NULL;
    }

    public function GetUniversities($personId=-1,$universityId=-1,$universityType=-1,$universityName='',$fromDate='',$toDate='')
    {
        $selectQuery = "SELECT universityId,personId,universityType,universityName,DATE(fromDate) AS fromDate,DATE(toDate) AS toDate FROM universities WHERE !isDeleted";
        if($personId !== -1)
            $selectQuery .= " && personId = ".$personId;
        if($universityId !== -1)
            $selectQuery .= " && universityid = ".$universityId;
        if($universityType !== -1)
            $selectQuery .= " && universitytype = ".$universityType;
        if($universityName !== '')
            $selectQuery .= " && universityName = '".$universityName."'";
        //todo:: add search by dates
        $selectQuery.= ";";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        return NULL;
    }

    public function GetEmployers($personId=-1,$employerId=-1,$employerName='',$fromDate='',$toDate='')
    {
        $selectQuery = "SELECT employersId,personId,employerName,DATE(fromDate) AS fromDate ,DAte(toDate) as toDate FROM employers WHERE !isDeleted";
        if($personId !== -1)
            $selectQuery .= " && personId = ".$personId;
        if($employerId !== -1)
            $selectQuery .= " && employerId = ".$employerId;
        if($employerName !== '')
            $selectQuery .= " && employerName = '".$employerName."'";
        //todo:: add search by dates
        $selectQuery.= ";";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        return NULL;
    }

    public function GetResearches($personId=-1,$researcheId=-1,$topicName='', $topicdesc='')
    {
        $selectQuery = "SELECT researcheId,personId,researcheName,researchesDesc FROM researches WHERE !isDeleted";
        if($personId !== -1)
            $selectQuery .= " && personId = ".$personId;
        if($researcheId !== -1)
            $selectQuery .= " && researcheId = ".$researcheId;
        if($topicName !== '')
            $selectQuery .= " && topicName LIKE '%".$topicName."%";
        if($topicdesc !== '')
            $selectQuery .= " && researchesDesc LIKE '%".$topicdesc."%";
        $selectQuery.= ";";
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
        return NULL;
    }

    public function DeleteUser($personId)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_DeletePerson(".$personId.",@personId);";
        $selectPersonIdQuery = "SELECT @personId";
        $resultInsertQuery = $this->db->query($insertQuery);
        if($resultInsertQuery)
        {
            $result = $this->db->query($selectPersonIdQuery);
            if($result)
            {
                while($row = $result->fetch_assoc())
                    $returnVal = $row['@personId'];
            }
        }
        return $returnVal;
    }

    public function DeleteAddress($personId,$addressId)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_DeleteAddress(".$addressId.",".$personId.");";
        $result = $this->db->query($insertQuery);
        if($result)
        {
            $returnVal = true;
        }
        return $returnVal;
    }

    public function DeleteEmail($personId,$addressId)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_DeleteEmail(".$addressId.",".$personId.");";
        $result = $this->db->query($insertQuery);
        if($result)
        {
            $returnVal = true;
        }
        return $returnVal;
    }

    public function DeleteEmployer($personId,$addressId)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_DeleteEmployer(".$addressId.",".$personId.");";
        $result = $this->db->query($insertQuery);
        if($result)
        {
            $returnVal = true;
        }
        return $returnVal;
    }

    public function DeletePhone($personId,$addressId)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_DeletePhoneno(".$addressId.",".$personId.");";
        $result = $this->db->query($insertQuery);
        if($result)
        {
            $returnVal = true;
        }
        return $returnVal;
    }

    public function DeleteResearch($personId,$addressId)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_DeleteResearch(".$addressId.",".$personId.");";
        $result = $this->db->query($insertQuery);
        if($result)
        {
            $returnVal = true;
        }
        return $returnVal;
    }

    public function DeleteUniversity($personId,$addressId)
    {
        $returnVal = null;
        $insertQuery = "CALL sp_DeleteUniversity(".$addressId.",".$personId.");";
        $result = $this->db->query($insertQuery);
        if($result)
        {
            $returnVal = true;
        }
        return $returnVal;
    }
    
    public function UpdatePerson($personId,$firstName,$lastName,$sex)
    {
        # code...sp_UpdatePerson
        $insertQuery = "CALL sp_UpdatePerson('".$firstName."','".$lastName."','".$sex."',".$personId.");";        
        $resultInsertQuery = $this->db->query($insertQuery);        
    }

    public function UpdateAddress($personId,$addressType,$firstLine,$lastLine,$city,$state,$zip,$addressId)
    {
        # code...sp_UpdateAddress
        $insertQuery = "CALL sp_UpdateAddress(".$personId.",".$addressType.",'".$firstLine."','".$lastLine."','".$city."','".$state."','".$zip."',".$addressId.");";        
        $resultInsertQuery = $this->db->query($insertQuery);
    }

    public function UpdatePhone($personId,$phoneType,$phoneno,$phonenoId)
    {
        # code...sp_UpdatePhoneno
        $insertQuery = "CALL sp_UpdatePhoneno(".$personId.",".$phoneType.",'".$phoneno."',".$phonenoId.");";
        $resultInsertQuery = $this->db->query($insertQuery);
    }

    public function UpdateEmail($personId, $emailType,$email,$id)
    {
        # code...sp_UpdateEmail
        $insertQuery = "CALL sp_UpdateEmail(".$personId.",".$emailType.",'".$email."',".$id.");";
        $resultInsertQuery = $this->db->query($insertQuery);
    }

    public function UpdateResearch($personId,$researchName,$reseachDescription,$researcheId)
    {
        # code...sp_UpdateResearch
        $insertQuery = "CALL sp_UpdateResearch(".$personId.",'".$researchName."','".$reseachDescription."',".$researcheId.");";
        $resultInsertQuery = $this->db->query($insertQuery);
    }

    public function UpdateEmployer($personId,$employerName,$fromDate,$toDate,$employerId)
    {
        # code...sp_UpdateEmployer
        $insertQuery = "CALL sp_UpdateEmployer(".$personId.",'".$employerName."','".$fromDate."',".$toDate.",".$employerId.");";
        $resultInsertQuery = $this->db->query($insertQuery);
    }

    public function UpdateUniversity($personId,$universityType,$universityName,$fromDate,$toDate,$universityId)
    {
        # code...sp_UpdateUniversity
        $insertQuery = "CALL sp_UpdateUniversity(".$personId.",".$universityType.",'".$universityName."','".$fromDate."',".$toDate.",".$universityId.");";
        $resultInsertQuery = $this->db->query($insertQuery);
    }

    public function GetTableInformation()
    {
        # code...sp_GetTableInformation
        $insertQuery = "CALL sp_GetTableInformation();";
        $result = $this->db->query($insertQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
    }

    public function DynamicSearch($input)
    {
        # code...
        $selectQuery = '';
        if($input->outTblId1 && !$input->outTblId2)
        {
            if($input->outColId1 && !$input->outColId2)
                $selectQuery = "SELECT ". $input->outColId1." AS '".$input->outColDisplayName1 ."' FROM ".$input->outTblId1." WHERE !isDeleted && personId in (SELECT personId FROM ".$input->inTbleId." WHERE !isDeleted && ".$input->inColId." LIKE '%".$input->inColVal."%');";
            else
                $selectQuery = "SELECT ". $input->outColId1." AS '".$input->outColDisplayName1."', ".$input->outColId2." AS '".$input->outColDisplayName2."' FROM ".$input->outTblId1." WHERE !isDeleted && personId in (SELECT personId FROM ".$input->inTbleId." WHERE !isDeleted && ".$input->inColId." LIKE '%".$input->inColVal."%');";
        }
        else
        {
            if($input->outColId1 && $input->outColId2)
                $selectQuery = "SELECT tbl1.". $input->outColId1." AS '".$input->outColDisplayName1."', tbl2.".$input->outColId2." AS '".$input->outColDisplayName2."' FROM ".$input->outTblId1." AS tbl1, ".$input->outTblId2." AS tbl2 WHERE !tbl1.isDeleted && tbl1.personId in (SELECT personId FROM ".$input->inTbleId." WHERE !isDeleted && ".$input->inColId." LIKE '%".$input->inColVal."%') && !tbl2.isDeleted && tbl2.personId in (SELECT personId FROM ".$input->inTbleId." WHERE !isDeleted && ".$input->inColId." LIKE '%".$input->inColVal."%');";
            else
                $selectQuery = "SELECT tbl1.". $input->outColId1." AS '".$input->outColDisplayName1."' FROM ".$input->outTblId1." AS tbl1, ".$input->outTblId2." AS tbl2 WHERE !tbl1.isDeleted && tbl1.personId in (SELECT personId FROM ".$input->inTbleId." WHERE !isDeleted && ".$input->inColId." LIKE '%".$input->inColVal."%') && !tbl2.isDeleted && tbl2.personId in (SELECT personId FROM ".$input->inTbleId." WHERE !isDeleted && ".$input->inColId." LIKE '%".$input->inColVal."%');";
        }
        $result = $this->db->query($selectQuery);
        if($result && $result->num_rows > 0)
        {
            while ($row = $result->fetch_assoc()) 
            {
                $data[] = $row;                
            }
            return ($data);
        }
    }
}

?>