import { Component, OnInit } from '@angular/core';
import { PostService } from '../shared/services/post.service';
import { Post } from '../shared/models/post.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'feed-view',
  templateUrl: './feed-view.component.html',
  styleUrls: ['./feed-view.component.css']
})
export class FeedViewComponent implements OnInit {
  public posts: Post[];

  constructor(private postService: PostService, private route: ActivatedRoute) {

  }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.loadPostsByFeed(<number>params.feedId);
    });

  }

  private loadPostsByFeed(feedId: number): void {

    if (!feedId) {
      return;
    }
    this.posts = null;
    this.postService.getUnreadPostsByFeed(feedId).subscribe((data: Post[]) => {
      this.posts = data;
    }, error => console.log(error));
  }
}

