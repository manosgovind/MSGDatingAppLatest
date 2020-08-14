import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() valueFromHome: any; // input is used to pass value from a parent component to a child component,
  // in this case passing value from home component to register component

  @Output() cancelRegister = new EventEmitter(); // @output is used to pass a value from child component to parent comp
  // tslint:disable-next-line: max-line-length
  // in this case we are passing a value to home compo. to let it know that we clicked cancel registration, @output should be an eventEmitter

  model: any = {};

  constructor(private authservice: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authservice.register(this.model).subscribe(() =>
      this.alertify.success('Registration Succesful'),
      error => this.alertify.error(error)
    );
  }
  cancel() {
    this.cancelRegister.emit(false); // emitting the output value to parent compo.
    this.alertify.message('cancelled!');
  }


}
