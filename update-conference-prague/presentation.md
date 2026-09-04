---
marp: true
title: Today's "Best Practices" of User Authentication
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

# Today's "Best Practices" of User Authentication

with AL Rodriguez

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Shameless Self Promotion

- @ProgrammerAL and https://ProgrammerAL.com
- Customer Success Engineer at Duende Software
- Freelance Affiliate at https://globalGlob.dev 
  - Index 0 for Dev News

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Why are we here?

- AuthN - Trusting Who a User Is
- Best Practices of AuthN

---

# What is a "Best Practice"?

- __*Usually*__ a good idea
- Can be the default
- Might not be what you need, depends on your use case
- When you're not sure:
  * pay a consultant

---

# Where to Start?

- OAuth?
  - 1.0 vs 2.0 vs 2.1?
- OIDC?
- Is SAML still secure?
* The consultant's answer is...
  * Depends on your scenario

---

# Scenario: Basic, Simple, Small App

- Users sign in to your Web App
  - Nothing Else
- No Backend API
  - Requests made directly through the Web App
- Users stored in your database

---

# What do you need?

- Nothing! 
- Use an OSS library to authenticate users

---

# Scenario: Today's Standard App

- Backend API and Front End Web UI
  - Maintained by separate teams
- Users stored somewhere else
  - SaaS platform
  - Custom internal service maintained by another team
  - Product: Duende IdentityServer, Keycloak

```mermain
architecture-beta
    service idp(cloud)[IdP]
    service client(server)[Web Client]
    service api(server)[API]
    
    client:R -- L:idp
```

---

# You Need OAuth!

* It Depends!
* Just Kidding, you need OAuth
  - Open Standard for Access Delegation
* User Data lives in dedicated service (IdentityProvider)
* User needs to sign in to 1+ clients

---

# Multiple Types of Tokens

- Access Token
  - Authorization info like scopes, groups, some user info
  - Short lifetime
- Refresh Token
  - Used to get a new Access Token
  - Long lifetime
- Identity Token
  - User Info, name, email, job title, etc

---

# OAuth Flow the User Sees

* User exists in IdentityProvider (IdP) like IdentityServer, Auth0, Entra, etc
* Client App has user to sign in - Redirects to IdP
* User signs in, Allows IdP to send their into to Client App
* Redirected to Client App with token containing user data

```mermaid
flowchart TD
    A[Client Redirects to IdP] --> B(User Signs In)
    B --> C[Allows Client Access]
    C --> D[Redirect to Client]
```
---

# Who Make Today's Best Practices?

- A Standards Body
- RFC 9700 - https://www.rfc-editor.org/info/rfc9700
  - Some things for App Dev, some for IdP

---

# Scenario: Attacker Gets an Access Token

- Real Token was Created for a Valid Use Case
  - A User Signed In
- Worst Case Scenario
  - This ends up in the news

---

# Mitigations: Minimize How Often Tokens Are Used

- Minimize Token Blast Radius
- Verify the Client
- Protect Refresh Token

---

# Mitigation: Minimize Token Blast Radius: Restrict Audience Claim to App

- Set the Token `aud` claim
- Retrieve new Token for Machine-to-Machine requests
- Stops attacker from using token on other APIs

---

# Mitigation: Minimize Token Blast Radius: Restrict Scopes to App Requirement

- Only requests Scopes the app Needs
- Stops attacker from using token on other endpoints

---

# Mitigation: Verify the Client: Confidential Clients

```text
Authorization servers SHOULD enforce client authentication if it is feasible
```

* Don't use Client Secret
* Client proves to Auth Server it is who it says it is
* Enable with mTLS or Signed Tokens
  - No client secret string
* https://duendesoftware.com/blog/20260903-client-secrets-mutual-tls-and-private-key-jwt

---

# Mitigation: Verify the Client: Confidential Clients: mTLS

- Purpose: 
- Mutual TLS
- Client and Auth Server validate each other with Certificates
- Most Complex Confidential Client

---

# Mitigation: Verify the Client: Confidential Clients: Signed JWT

- Purpose: 
- Client has Public/Private Key
- Auth Server knows the Public Key
- Client signs request with Private Key
  - Auth Server validates with Public Key

---

# Mitigation: Verify the Client: Demonstrating Proof of Possession (DPoP)

- Purpose: 
- Client includes DPoP Proof in Initial Request for Token
- Auth Server binds Access Token to Public Key from the DPoP Proof
- For Every Request to API, Client Includes DPoP Proof, API Validates Against Auth Server

- https://duendesoftware.com/blog/20251216-security-lingo-explained-dpop

---

# Mitigation: Protect Refresh Token

- Refresh Tokens == High Value
- Rotate on each use

---

# Mitigation: Protect Refresh Token: Extra Credit

- Use Server-Side Sessions
  - Revoke tokens if leak suspected

---

# ???Other Recommendations???

- Use Auth Server Metadata
  - Don't hard code anything

TODO: Code Sample with IS
TODO: Is this needed? Should it be deleted or moved?

---

# Scenario: Attacker Can See Requests
<!-- # Best Practice: Protect at Request Level -->

- Attacker in the Middle

- Interactive flow of user signing-in
- Malicious Actor Intercepts the Token
- Forces Browser to Make Silent Request to Website

```mermaid
architecture-beta
    service idp(cloud)[IdP]
    service client(server)[Web Client]
    service api(server)[API]
    service bad(cloud)[Malicious Actor]
    
    client:R -- L:bad
    bad:R -- L:idp
```

---

# Mitigations: Don't Let Attacker Replay Requests

- Cross Site Request Forgery (CSRF)
- Nonce
- PKCE

---

# CSRF

```text
Clients MUST prevent Cross-Site Request Forgery (CSRF)...requests to the redirection endpoint that do not originate at the authorization server, but at a malicious third party...
```
- Malicious Site with hidden link is
  - Ex: https://important-site.com/callback?code=ATTACKER_CONTROLLED_CODE
- Random string to gate future request
  - Request blocked if string is wrong
- Stops request replay attacks
- Simple and effective

TODO: Diagram

---

# Nonce

- Purpose: Ensure Final Token from Auth Server
- Client generates random string `nonce`, includes in initial auth request
- Final Token includes `nonce`
- Client validates the Token `nonce` matches value in original request

TODO: Diagram

---

# PKCE

- Purpose: Ensure same client used for all Auth requests during redirects
- Client generates random string `code_verifier`, includes in requests to Auth Server
- Auth Server doesn't return `code_verifier`
  - `code_verifier` can't be intercepted by response

TODO: Diagram

---

# Best Practice: Don't Alow Token Replay

- mTLS
- DPoP
- Cycle Refresh Token on each use
- Access tokens should be audience restricted to application TODO: JWT sample

---

# Even Better Practice: Don't Send Tokens to Uncontrolled Endpoints

- ie, Only send tokens to your endpoints

```mermaid
architecture-beta
    service idp(cloud)[IdP]
    service client(server)[Web Client]
    service api(server)[API]
    
    client:R -- L:api
    api:R -- L:idp
```

---

# Scenario: 

- Bad Redirect
- Clickjacking aka User Interface Redressing

---

# Mitigations: Ensure Client Communicates with You

- Don't let client choose redirectors (Avoid HTTP 307)
- Context Security Policy (CSP)

---

# Mitigation: Ensure Client Communicates with You: Avoid HTTP 307

1. User submits credentials
1. Auth Server Accepts, returns a Redirect

- With HTTP 307 (Temporary Redirect), same request sent
  - Includes user credentials
- Mitigation: Use HTTP 302 (Found)
  - New request, doesn't include credentials

---

# Mitigation: Ensure Client Communicates with You: CSP

- For Clickjacking Attack
- CSP: Website can only talk to known endpoints

---

# Security Profiles

- For specific scenarios
- Profiles
  - FAPI

---

# FAPI 2.0

- Security Profile targeted towards High Value scenarios
  - Financial, Health, Government
- 
- https://openid.net/specs/fapi-security-profile-2_0-final.html

---

# ???

---

# A.I.?

---

# Review

- Secure your clients
- Don't let requests get replayed
- ???

![bg right 80%](presentation-images/presentation_link_qrcode.png)
