import { Component, OnInit } from '@angular/core';
import { PostService } from '../shared/services/post.service';
import { Post } from '../shared/models/post.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'category-view',
  templateUrl: './category-view.component.html',
  styleUrls: ['./category-view.component.css']
})
export class CategoryViewComponent implements OnInit {
  public posts: Post[];
  public category: string;
  public countValue: number = 25;

  constructor(private postService: PostService, private route: ActivatedRoute) {

  }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {

      if (!params.category) {
        this.category = "All";
        this.loadLatestPosts();
        return;
      }


      this.category = params.category;
      this.loadPostsByCategory(this.category);
    });
  }

  private loadPostsByCategory(category: string): void {

    if (!category) {
      return;
    }

    this.posts = null;
    this.postService.getUnreadPostsByCategory(category).subscribe((data: Post[]) => {
      this.posts = data;
    }, error => console.log(error));
  }

  private loadLatestPosts(): void {
    this.posts = null;

    this.postService.getAllUnreadPosts().subscribe((data: Post[]) => {
      this.posts = data;
    }, error => console.log(error));
  }
}

