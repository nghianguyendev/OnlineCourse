import { Component, OnInit, ViewChild } from '@angular/core';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  constructor() { }
  name: string;
  @ViewChild('f') form: any;
  
  ngOnInit() {
    this.name = "sample name";
    this.testPromise();
  }

  onSubmit() {
    if (this.form.valid) {
      console.log("Form Submitted!");
      this.form.reset();
      this.name="reset";
    }
  }

  testPromise()
  {
    Promise.resolve('done')
  .then(
    (val) => console.log(val),
    (err) => console.error(err)
  );
  }
}
