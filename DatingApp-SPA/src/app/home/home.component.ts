import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  RegMode = false;
  vals: any;
  constructor(private http: HttpClient ) { }

  ngOnInit() {
     this.getValues();
  }
  RegModeON() {
    this.RegMode = true;
  }

  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(resp => {
      this.vals = resp;
    }, err => { console.log(err);
    });

  }

  CancelRegMode(RegnMode: boolean)
  {
    this.RegMode = RegnMode;
  }

}
