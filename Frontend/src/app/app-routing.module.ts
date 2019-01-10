import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CourseComponent } from './components/course/course.component';
import { HomeComponent } from './components/home/home.component';
import { CategoryComponent } from './components/category/category.component';
import { AuthGuard } from './_guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { CourseregistrationComponent } from './components/courseregistration/courseregistration.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
 { path: 'course', component: CourseComponent,canActivate: [AuthGuard] },
 { path: 'courseregistration', component: CourseregistrationComponent,canActivate: [AuthGuard] },
 { path: 'category', component: CategoryComponent, canActivate: [AuthGuard] },
 { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

