import { OnInit, OnDestroy, Component, Input, Renderer2, ElementRef, ViewChild } from "@angular/core";


@Component({
  selector: 'progress-bar',
  templateUrl: './progress-bar.component.html',
  styleUrls: ['/progress-bar.component.css']
})
export class ProgressBarComponent implements OnInit, OnDestroy {

  constructor(private rederer2: Renderer2) { }

  ngOnInit(): void {
    console.log(`Init progress ${this.currentProgress}`);
    this.start();
  }

  ngOnDestroy(): void {
    console.log("Destroy progress");
    window.clearInterval(this.interval);
  }

  public currentProgress: number = 1;
  public interval: any;
  public current: 1;

  @Input()
  public step: number = 1;

  @Input()
  public height: number = 5;

  @ViewChild('progressBar') progressBar: ElementRef;

  public get heightString(): string {
    return `${this.height}px`;
  }

  private start(): void {
    let self = this;
    this.interval = window.setInterval(function () {

      if (self.currentProgress === 100) {
        self.currentProgress = 0;
      } else {
        self.currentProgress = 100;
      }

    }, 800);
  }
}
