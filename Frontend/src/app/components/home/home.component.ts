import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/shared/services/course.service';
import { AppState } from 'src/app/app.state';
import { Store } from '@ngrx/store';
import { store } from '@angular/core/src/render3';
import { CourseModel } from 'src/shared/models/course.model';
import { CREATE_COURSE, CreateCourse } from 'src/app/actions/course.action';
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private courseService: CourseService,
    private store:Store<AppState>) { }

  ngOnInit() {

    this.courseService.getAll().pipe(
    map((payload) =>{
        return new CreateCourse(payload)
    }))
    .subscribe(courses => { 
      this.store.dispatch(courses);
    });
  }

}
