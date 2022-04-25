import React, { useState, useEffect, Component } from 'react';
import { Container } from 'react-bootstrap';
import { Dependent } from '../models/Dependent';
import { Employee } from '../models/Employee';
import { EmployeeCosts } from '../models/EmployeeCosts';
import EmployeeService from '../service/EmployeeService';
import './Employer.css';

function Employer() {

  const [result, setResult] = useState<Employee[]>([]);
  const [selectedOption, setSelectedOption] = useState(1);
  const [dependents, setdependents] = useState<Dependent[]>([]);
  const [employeeCosts, setemployeeCosts] = useState(new EmployeeCosts());
  const headings = ['Name', 'Relation'];
  const employeeService = new EmployeeService();

  const request = async () => {
    const data = await employeeService.getAllEmployees();
    setResult(data);
    console.log(result);
    return data
  };


  useEffect(() => {
    console.log('rendering finished!');
    request();
  }, []);

  useEffect(() => {
    if (dependents.length == 0) {
      setDependentValues(1);
      console.log("Set default dependent values")
    }
  }, [result]);

  const getEmployeeBenefitsCosts = async () => {
    const data = await employeeService.getEmployeeBenefitsCosts(selectedOption);
    setemployeeCosts(data);
    return data
  }

  const setDependentValues = (id: number) => {
    setSelectedOption(id);
    var dependents = result.find(a => a.id.toString() === id.toString())?.dependents as Dependent[];
    if (dependents != null) {
      setdependents(dependents);
    }
    else {
      setdependents([])
    }
  }

  const handleChange = (e) => {
    console.log(e.target.value);
    setDependentValues(e.target.value);
  }

  return (

    <Container style={{ paddingBottom: '20px' }} fluid>

      <div>
        <br></br>
        <br></br>
        Select Employee :

        <select style={{ borderColor: "orangered" }} value={selectedOption} onChange={handleChange}>
          {result.map((option) => (
            <option value={option.id}>{option.name}</option>
          ))}
        </select>
        <br>
        </br>
        <br></br>
        <b><u>Costs</u></b>
        <br></br>
        <br></br>
        <button className='button' onClick={getEmployeeBenefitsCosts}>Get Employee Benefits Cost</button>
        <br></br>
        <br></br>
        <table>
          <tr>
            <th>Name</th>
            <th>DependentCount</th>
            <th>AnnualSalary</th>
            <th>MonthlySalary</th>
            <th>TotalDeductions</th>
            <th>FinalAnnualSalary</th>
            <th>FinalMonthlySalary</th>
            <th>PayChecksPerYear</th>
          </tr>
          <tr>
            <td>{employeeCosts.name}</td>
            <td>{employeeCosts.dependentCount}</td>
            <td>{employeeCosts.annualSalary}</td>
            <td>{employeeCosts.monthlySalary}</td>
            <td>{employeeCosts.totalDeductions}</td>
            <td>{employeeCosts.finalAnnualSalary}</td>
            <td>{employeeCosts.finalMonthlySalary}</td>
            <td>{employeeCosts.payChecksPerYear}</td>
          </tr>

        </table>
      </div>
    </Container>
  )
}

export default Employer;


