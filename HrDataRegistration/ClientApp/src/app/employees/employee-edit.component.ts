import { Component, OnInit } from '@angular/core';
import { DataService } from '../core/data.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IEmployee } from '../shared/interfaces';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html'
})

export class EmployeeEditComponent implements OnInit {
  employeeForm: FormGroup;
  employee: IEmployee;
  errorMessage: string;

  constructor(private dataService: DataService,
    private router: Router) { }

  ngOnInit() {
    this.initForm();
  }

  private initForm() {
    const validPhonePattern = /[0-9]{0,14}/;
    const nationalIdPattern = /(\d{6})-?[012]\d{4}/;

    this.employeeForm = new FormGroup({
      'firstName': new FormControl(null, Validators.required),
      'lastName': new FormControl(null, Validators.required),
      'socialSecurityNumber': new FormControl(null, [Validators.pattern(nationalIdPattern), Validators.required]),
      'phoneNumber': new FormControl(null, Validators.pattern(validPhonePattern))
    });
  }

  log(message) {
    console.log(message);
    return true;
  }

  submit({ value, valid }: { value: IEmployee, valid: boolean }) {
    this.dataService.addEmployee(value)
      .subscribe((employee: IEmployee) => {
        if (employee) {
          this.router.navigate(['/employees']);
        }
        else {
          this.errorMessage = 'Unable to add customer';
        }
      },
        (err: any) => console.log(err));
  }

  cancel() {
    this.router.navigate(['/employees']);
  }
}
