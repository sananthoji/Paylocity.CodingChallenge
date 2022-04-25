import { Employee } from "../models/Employee";
import { EmployeeCosts } from "../models/EmployeeCosts";
import {Buffer} from 'buffer';

export class EmployeeService {

    public async getAllEmployees(): Promise<Employee[]> {
        
        const apiUrl = process.env.REACT_APP_API_EMPLOYEE_ENDPOINT;
        const response = await fetch(apiUrl, { method: 'GET' })
        return await response.json();
    }

    public async getEmployeeBenefitsCosts(id: number): Promise<EmployeeCosts> {

        const apiUrl = process.env.REACT_APP_API_EMPLOYEE_ENDPOINT + 
        '/GetEmployeeDeduction?employeeId=' + id;
        const response = await fetch(apiUrl, { method: 'GET' })
        return await response.json();
    }

}
export default EmployeeService