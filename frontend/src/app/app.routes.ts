import { Routes } from '@angular/router';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { HomePageComponent } from './home-page/home-page.component';
import { NewPostComponent } from './new-post/new-post.component';
import { SelectAuthorComponent } from './select-user/select-author.component';

export const routes: Routes = [
  {
    path: '',
    component: HomePageComponent
  },
  {
    path: 'blogpost/:id',
    component: BlogPostComponent
  },
  {
    path: 'new-post',
    component: NewPostComponent
  },
  {
    path: 'select-author',
    component: SelectAuthorComponent
  }

];
