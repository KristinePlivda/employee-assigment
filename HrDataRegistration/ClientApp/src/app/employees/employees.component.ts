import { Component, OnInit } from '@angular/core';

import { DataService } from '../core/data.service';
import { IEmployee } from '../shared/interfaces';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html'
})

export class EmployeesComponent implements OnInit {
  employees: IEmployee[];

  constructor(private dataService: DataService,
              private router: Router) { }

  ngOnInit() {
    this.dataService.getEmployees()
      .subscribe((data: IEmployee[]) => this.employees = data);
  }

  goToEmployeeEdit() {
    this.router.navigate(['/employee-edit']);
  }
}
