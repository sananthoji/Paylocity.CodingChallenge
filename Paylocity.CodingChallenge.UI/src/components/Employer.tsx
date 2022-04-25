import React, { useState, useEffect, Component } from 'react';
import { Container } from 'react-bootstrap';
import { Employee } from '../models/Employee';
import { EmployeeCosts } from '../models/EmployeeCosts';
import EmployeeService from '../service/EmployeeService';
import './Employer.css';

function Employer() {

  const [result, setResult] = useState<Employee[]>([]);
  const [selectedOption, setSelectedOption] = useState(1);
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
      setSelectedOption(1);
  }, [result]);

  const getEmployeeBenefitsCosts = async () => {
    const data = await employeeService.getEmployeeBenefitsCosts(selectedOption);
    setemployeeCosts(data);
    return data
  }

  

  const handleChange = (e) => {
    console.log(e.target.value);
      setSelectedOption(e.target.value);
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
            <th>Dependent Count</th>
            <th>Annual Salary</th>
            <th>Monthly Salary</th>
            <th>Total Deductions</th>
            <th>Final AnnualSalary</th>
            <th>Final MonthlySalary</th>
            <th>Pay ChecksPerYear</th>
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


