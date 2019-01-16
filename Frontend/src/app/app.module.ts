import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { CourseComponent } from './components/course/course.component';
import { HomeComponent } from './components/home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoryComponent } from './components/category/category.component';
import { CourseService } from 'src/shared/services/course.service';
import { SecureApi } from 'src/core/secure.api';
import { HttpModule } from '@angular/http';
import { ENV_PROVIDERS } from 'src/environments/environment';
import { Count } from 'src/shared/pipes/count.pipe';
import { LoginComponent } from './components/login/login.component';
import { AuthenticationService } from 'src/shared/services/authentication.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { reducer } from './reducers/course.reducer';
import { CourseregistrationComponent } from './components/courseregistration/courseregistration.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    CourseComponent,
    HomeComponent,
    CategoryComponent,
    Count,
    LoginComponent,
    CourseregistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    HttpClientModule, 
    StoreModule.forRoot({course: reducer}), ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [
    CourseService,
    SecureApi,
    ENV_PROVIDERS,
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
