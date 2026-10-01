---
marp: true
title: Over-Engineering globalGlob.dev: The Bad Ideas that got a 100 Lighthouse Score
paginate: true
theme: default
author: AL Rodriguez
footer: '@ProgrammerAL'
---

<style>
section::before {
  content: url('https://raw.githubusercontent.com/ProgrammerAL/Presentations-2026/main/common-images/duende-logo-rebranded.svg');
  transform: scale(.25);
  position: absolute;
  right: -320px;
  bottom: -65px;
}
</style>

# Over-Engineering globalGlob.dev: The Bad Ideas that got a 100 Lighthouse Score

with AL Rodriguez

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Duende Software

- Full Disclosure: They pay me (but I like them anyway)
  - Customer Success Engineer

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Shameless Self Promotion

- @ProgrammerAL and https://ProgrammerAL.com
- Freelance Affiliate at https://globalGlob.dev 
  - Index 0 for Dev News

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Content Warning

- Bad Ideas Ahead

---

# globalGlob(*\*/\*)

- https://globalGlob.dev
- Secret: Satire
- Articles/Videos/Newsletter
- Deployed December 2025

---

# (2018) What if I made my own static site generator?

- And made it crazy?!

---

# My Vision

- Blog Site
- Users upload a post
- Site generates the HTML, stores in cloud storage
- All requests receive that HTML
- Started work in 2017

---

# There were problems

- How do you update the HTML?
- How do you update styles?
- What does/doesn't get cached?
- Idea Abandoned: 2018

---

# (Today) Typical Static Site Generator

- Build Time
- Astro/Hugo/Jekyll/11ty

---

# (Today) Typical CMS

- Live Edits
- TinyCMS/Pico/WonderCMS/StaticCMS

---

# 2025 - I want to make jokes

- Jokes...on the internet
- 8 year old idea
  - Bad then, great now

---

# Goals

- Fast Site
- Do everything "right"
  - Security Headers, Static HTML, Minified and Cached Content
- Lighthouse Score 100
  - Not 99!

---

# What it Lighthouse?

- Audits Website Metrics
  - Emphasis: User Experience
  - Fast server response, security headers, valid HTML, etc
- Developed by Google

![bg right 80%](presentation-images/lighthouse-image.png)

---

# Social Media Scores

- October 1, 2026 on Chrome Desktop

|           | Performance | Accessibility | Best Practices | SEO |
|-----------|-------------|---------------|----------------|-----|
| Bluesky   | 72          | 80            | 92             | 92  |
| Instagram | 67          | 79            | 58             | 92  |
| Twitter   | 69          | 63            | 77             | 92  |
| LinkedIn  | 65          | 90            | 73             | 83  |



---

# Misc Astro Sites

- October 1, 2026 on Chrome Desktop

|                           | Performance | Accessibility | Best Practices | SEO |
|---------------------------|-------------|---------------|----------------|-----|
| designcember.web.app      | 99          | 88            | 96             | 100 |
| ikea.com                  | 98          | 96            | 100            | 100 |
| developers.cloudflare.com | 99          | 89            | 96             | 92  |
| docs.duendesoftware.com   | 98          | 100           | 77             | 92  |

---

# Reaching Maximum Lighthouse

- Make it Fast
  - Static HTML
- Make it Right
  - Use Valid HTML
  - Add Security Headers
- https://jsonld.com/perfect-lighthouse-100-scores

---

# globalGlob(*\*/\*) Website

- Frontend
  - Static site
  - Main Pages Static Code
    - /about, /staff, /disclaimer
  - Dynamic Pages are generated when published or updated
    - Articles, Index, and Category pages

- Backend
  - Server-side functionality to retrieve data

---

# Admin Functionality

- Accidental CMS
- Add/Remove content
- Re-generate static content
- Publisher Frontend
  - Blazor WASM
  - Don't care about lighthouse
- Publisher Backend
  - ASP.NET Core/Azure CosmosDB

---

# Static Site

- ???
- Harder than you think
- Mix Static with Dynamic

---

# Static Site - The globalGlob.dev Way

- Some Static Pages
  - /about, /disclaimer, /staff
- Some `Lit` modules
  - Some loose TypeScript
- Some pages generated at runtime, placed in cloud storage
  - /index, /articles, every "paging" page

---

# Static Site - The globalGlob.dev Way - Static Pages

- Hand written
- Minimal '`Lit`' modules
  - Header and Footer
- CSS file per page
  - Reduces accidental bloat

---

# globalGlob.dev Dynamic Page Content Strategy

![bg right 90%](presentation-images/cache-architecture.svg)

<!-- 
architecture-beta
    service site(server)[Site]
    service siteApi(server)[Site API]
    service storageApi(server)[Storage API]
    service publisherApi(server)[Publisher API]
    service r2(disk)[Cloudflare R2]
    service kv(disk)[Cloudflare KV]

    site:R -- L:siteApi
    siteApi:T -- B:r2
    r2:R -- L:storageApi
    siteApi:T -- B:kv
    siteApi:R -- L:publisherApi
    publisherApi:T -- B:storageApi

    align row kv r2 storageApi
    align row site siteApi publisherApi
-->

---

# Dynamic Content - The globalGlob.dev Way

- Site Header is *sometimes* dynamic
  - Index and other static pages
  - Reset each time a generated page is re-generated
- Re-generate page when needed, or want to

---

# Limit Dynamic Content

- Repaints Lower Performance Score
- Header repaint was costly

---

# Optimize the UI - Caching

- Cache Somewhere
- O(1) Lookup

---

# What to cache?

- Cache responses in Client Browser
- Cache responses in CDN
- Cache HTML in Cloud Storage

---

# Cache Responses - The globalGlob.dev Way

1. Cache in client browser
1. Cache in CDN
1. Cache in Persistent Store

---

# Cache Responses - The globalGlob.dev Way

- Use '`etag`' heavily
- Remove query string

---

# Cache Responses - The globalGlob.dev Way - ETag

- Arbitrary string for
  - Cache Id
  - globalGlob uses timestamp content is generated

---

# globalGlob.dev Page Cache Strategy

![bg right 50%](presentation-images/cache-flow.svg)

<!-- 
flowchart TD
    A[Request] -\->|Get money| B(Load Version Number from Storage)
    B -\-> C{Version == ETag?}
    C -\->|Yes| D(Return 304 Not Modified)
    C -\->|No| E{Version Exists?}
    E -\->|No|F[Re-Generate Page - Store with Version]
    E -\->|Yes|G[Return HTML]
    F -\-> G
-->

---

# Cache Responses - The globalGlob.dev Way - Query String

- Remove Query String
  - '`?a=123&b=456`' vs '`?b=456&a=123`'
  - https://calendar.perfplanet.com/2025/fixing-the-url-params-performance-penalty
- Anyone can add query string
  - https://community.cloudflare.com/t/facebook-now-adds-fbclid-query-string-to-urls-busting-cloudflares-cache/40355
- Cloudflare setting to force it off

---

# Compress Content

- Choose compression Algorithm
  - Brotli and Zstd
- From testing:
  - Brotli compresses smaller
  - Zstd compresses/decompresses faster, size pretty close to Brotli

---

# Compress Content - The globalGlob.dev Way

- Rely on Cloudflare to compress content per request
  - Text Only
  - HTML/CSS/JS/SVG
- Cloudflare doesn't compress images in realtime
  - Add code to pre-compress images - zstd

---

# Javascript Frameworks

- From https://cdnjs.com on September 30, 2026

| Framework                 | Raw Size | Brotli Compressed |
|---------------------------|----------|------------|
| React - 19.2.8 (minified) | 9.6 kB   | 3.5 kB     |
| Vue - 3.5.43              | 600.9 kB | 109 kB     |
| Ember - 6.12.0            |  2 MB    | 322 kB     |
| Preact - 10.29.6          |  11 kB   | 5.1 kB     |

---

# Javascript Frameworks - The globalGlob.dev Way

- JS per page
- Prefer pre-generated content over JS updates

---

# Static Content - The globalGlob.dev Way

- Make it small
- Make it load fast

---

# Static Content - The globalGlob.dev Way

1. Minify CSS
1. Minify JS
1. Generate HTML from template
1. Insert other inline HTML - if needed
1. Inline CSS/JS into HTML
1. Minify this
1. Reminder: This also gets compressed in response

---

# Images

- Smaller files are better
  - Content: Prioritize SVG
  - Otherwise, whatever's smaller
  - Use '`sizes`' property for "responsive" images
    - https://piccalil.li/blog/the-end-of-responsive-images
- Lazy Load
  - https://web.dev/articles/browser-level-image-lazy-loading

---

# Images - The globalGlob.dev Way

- Pre-compress with zstd
- Generate multiple images for size range (small, medium, large)
  - For non-SVG

---

# Fonts

- Use fonts already installed on device
- https://modernfontstacks.com

---

# Misc Lighthouse Rules

- Clean Site
  - No console errors, HTTPS for all scripts, no blocked scripts (trackers/analytics)
- Standards Compliant '`robots.txt`' file

---

# Review

- Pre-generate content
- Responsive Images
- 

![bg right 80%](presentation-images/presentation_link_qrcode.png)
