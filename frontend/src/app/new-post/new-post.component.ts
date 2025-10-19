import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import EasyMDE from 'easymde';
import { BlogPost, BlogPostService, CreateBlogPost } from '../blog-post.service';
import { EasyMdeEditorComponent } from '../easymde-editor/easymde-editor.component';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-new-post',
  imports: [FormsModule, EasyMdeEditorComponent],
  templateUrl: './new-post.component.html',
  styleUrl: './new-post.component.css'
})
export class NewPostComponent implements OnInit, OnDestroy {
  @ViewChild('editor') editorElement!: ElementRef<HTMLTextAreaElement>;
  blogPost: any

  constructor(private blogPostService: BlogPostService, private cookieService: CookieService){}
  ngOnDestroy(): void {
  }

  ngOnInit() {
    this.removeContentStyling();
  }

  onSubmit(form: NgForm) {
    let post: CreateBlogPost = {content: form.value.content, title: form.value.title, authorId: Number(this.cookieService.get('author'))}
    this.blogPostService.create(post).subscribe(response => {
      console.log('Got data: ', response);
    });

    console.log(form.value);
  }

  removeContentStyling() {
    const content: HTMLCollectionOf<Element> = document.getElementsByClassName('content');
    content[0].classList.add('remove-content');
  }
}
