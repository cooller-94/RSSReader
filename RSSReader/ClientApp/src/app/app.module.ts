import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FeedService } from './shared/services/feed.service';
import { AppSettings } from './shared/services/app-config.service';
import { PostService } from './shared/services/post.service';
import { FeedViewComponent } from './feed-view/feed-view.component';
import { TitleOnlyTableComponent } from './shared/components/title-only-table/title-only-table.component';
import { CategoryViewComponent } from './category-view/category-view.component';
import { ProgressBarComponent } from './shared/components/progress/porgress-bar.component';
import { StateService } from './shared/services/state.service';
import { SyncFeedService } from './shared/services/sync-feed.service';
import { WelcomeComponent } from './welcome/welcome.component';
import { LoginComponent } from './account/login/login.component';
import { AuthGuard } from './auth/guards/auth.guard';
import { ErrorInterceptor } from './shared/interceptors/error.interceptor';
import { AuthService } from './auth/services/auth.service';
import { TopNavMenuComponent } from './top-nav-menu/top-nav-menu.component';
import { JwtInterceptor } from './shared/interceptors/jwt.interceptor';
import { SignUpComponent } from './account/sign-up/sign-up.component';
import { UserService } from './shared/services/user.service';
import { NgbDropdownModule, NgbModal, NgbModalModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { FindFeedComponent } from './find-feed/find-feed.component';
import { SearchFeedCardComponent } from './search-feed-card/search-feed-card.component';
import { SelectCategoryComponent } from './shared/components/select-category/select-category.component';
import { CategoryService } from './shared/services/category.service';

export function initializeApp(appConfig: AppSettings) {
  return () => appConfig.load();
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FeedViewComponent,
    TitleOnlyTableComponent,
    CategoryViewComponent,
    ProgressBarComponent,
    WelcomeComponent,
    LoginComponent,
    SignUpComponent,
    TopNavMenuComponent,
    FindFeedComponent,
    SearchFeedCardComponent,
    SelectCategoryComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AngularSvgIconModule,
    NgbDropdownModule.forRoot(),
    NgbModalModule.forRoot(),
    NgbTypeaheadModule.forRoot(),
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'latest' },
      { path: 'feeds/:feedId', component: FeedViewComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'categories/:category', component: CategoryViewComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'latest', component: CategoryViewComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'welcome', component: WelcomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'find', component: FindFeedComponent, pathMatch: 'full', canActivate: [AuthGuard]  },
      { path: 'sign-up', component: SignUpComponent, pathMatch: 'full' },
    ])
  ],
  entryComponents: [SelectCategoryComponent],
  providers: [AppSettings,
    { provide: APP_INITIALIZER, useFactory: initializeApp, deps: [AppSettings], multi: true },
    FeedService,
    PostService,
    StateService,
    SyncFeedService,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    AuthService,
    AuthGuard,
    UserService,
    CategoryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
