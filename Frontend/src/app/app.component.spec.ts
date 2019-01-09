import { TestBed, async, fakeAsync, tick } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { CourseComponent } from './components/course/course.component';
import { Router } from '@angular/router';
import { Location } from '@angular/common'
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Count } from 'src/shared/pipes/count.pipe';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CategoryComponent } from './components/category/category.component';
import { ENV_PROVIDERS } from 'src/environments/environment';
import {routes} from 'src/app/app-routing.module'
import { LoginComponent } from './components/login/login.component';

describe('Router: AppComponent', () => {
  let location: Location;
  let router: Router;
  let fixture;
  
    beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        FormsModule,
        RouterTestingModule.withRoutes(routes),
        HttpClientModule
      ],
      declarations: [
        AppComponent,
        HomeComponent,
        CategoryComponent,
        HeaderComponent,
        FooterComponent,
        CourseComponent,
        LoginComponent,
        Count
      ],
      providers:[
        ENV_PROVIDERS
      ]      
    }).compileComponents();

    router = TestBed.get(Router); 
    location = TestBed.get(Location);

    fixture = TestBed.createComponent(AppComponent);
    router.initialNavigation();

  });

  it('navigate to "category" redirects you to /category', fakeAsync(() => {
    router.navigate(['category']);
    tick();
    expect(location.path()).toBe('/category');
  }));
  
});
