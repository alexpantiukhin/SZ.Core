using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;

using SZ.Core.Constants;

namespace SZ.Core.Models.Db
{
    internal static class InitilizeDb
    {
        public static void Init(ModelBuilder builder)
        {
            //Zemstvo sz = new Zemstvo
            //{
            //    Id = Settings.Zemstvos.SZId,
            //    Name = "Северодвинское Земство",
            //    Circle = 1,
            //    QuorumMeetingTen = 66.66,
            //    QuorumVotingTen = 66.66,
            //    QuorumTensForQuestion = 66.66,
            //    RequirePaperCircle = 2,
            //    Type = "Земства"
            //};

            //builder.Entity<Zemstvo>().HasData(sz);
            IdentityRole<Guid> roleAdmin = new IdentityRole<Guid>
            {
                Id = Settings.Roles.AdminId,
                Name = Settings.Users.AdminUserName,
                NormalizedName = Settings.Users.AdminUserName,
                ConcurrencyStamp = Settings.Roles.AdminConcurrencyStamp
            };

            builder.Entity<IdentityRole<Guid>>().HasData(roleAdmin);

            User admin = new User
            {
                Id = Settings.Users.AdminId,
                FirstName = Settings.Users.AdminUserName,
                SecondName = Settings.Users.AdminUserName,
                Patronym = Settings.Users.AdminUserName,
                PasswordHash = Settings.Users.AdminPasswordHash,
                SecurityStamp = Settings.Users.AdminSecurityStamp,
                ConcurrencyStamp = Settings.Users.AdminConcurrencyStamp,
                Gender = true
            };
            builder.Entity<User>().HasData(admin);

            IdentityUserRole<Guid> userRole = new IdentityUserRole<Guid>
            {
                RoleId = Settings.Roles.AdminId,
                UserId = Settings.Users.AdminId
            };
            builder.Entity<IdentityUserRole<Guid>>().HasData(userRole);

            //Document lastDocument = null;

            //Document docStatementInput = new Document
            //{
            //    Id = 1,
            //    DTCUTC = new DateTime(2019, 11, 26),
            //    DTCUTCAccept = new DateTime(2019, 11, 26),
            //    Status = EnumDocumentStatus.Confirmation,
            //    Type = EnumDocumentType.StatementInput,
            //    UserCreatorId = admin.Id,
            //    ZemstvoId = sz.Id,
            //};
            //builder.Entity<Document>().HasData(docStatementInput);

            //lastDocument = docStatementInput;

            //DocumentUser lastDocUser = null;

            //DocumentUser docUserStatementInput = new DocumentUser
            //{
            //    Id = 1,
            //    UserId = admin.Id,
            //    DocumentId = docStatementInput.Id
            //};
            //builder.Entity<DocumentUser>().HasData(docUserStatementInput);

            //lastDocUser = docUserStatementInput;

            //Position secretarHand = new Position
            //{
            //    Id = EnumPositions.SecretariatHand,
            //    Name = "Глава секретариата",
            //    Type = EnumPositionType.Selection,
            //    IsRecipientStatement = true
            //};
            //builder.Entity<Position>().HasData(secretarHand);
            //Position secretar = new Position
            //{
            //    Id = EnumPositions.Secretar,
            //    Name = "Cекретарь",
            //    Type = EnumPositionType.Decision,
            //    IsPosibleMany = true
            //};
            //builder.Entity<Position>().HasData(secretar);
            //Position hand = new Position
            //{
            //    Id = EnumPositions.Hand,
            //    Name = "Глава земства",
            //    Type = EnumPositionType.Selection,
            //    IsRecipientStatement = true
            //};
            //builder.Entity<Position>().HasData(hand);
            //Position seniorSecretar = new Position
            //{
            //    Id = EnumPositions.SenjorSecretar,
            //    Name = "Старший секретарь",
            //    Type = EnumPositionType.Decision,
            //    IsRecipientStatement = false,
            //    IsPosibleMany = true
            //};
            //builder.Entity<Position>().HasData(seniorSecretar);

            //var test = true; // isTest;

            //Document decisionSecretarHand = new Document
            //{
            //    Id = docStatementInput.Id + 1,
            //    DTCUTC = new DateTime(2019, 11, 26),
            //    DTCUTCAccept = new DateTime(2019, 11, 26),
            //    Status = EnumDocumentStatus.Confirmation,
            //    Type = EnumDocumentType.DecisionAppointmentPosition,
            //    UserCreatorId = admin.Id,
            //    ZemstvoId = sz.Id,
            //    PositionId = EnumPositions.SecretariatHand
            //};
            //builder.Entity<Document>().HasData(decisionSecretarHand);

            //lastDocument = decisionSecretarHand;

            //DocumentUser documentUser = new DocumentUser
            //{
            //    Id = docUserStatementInput.Id + 1,
            //    UserId = admin.Id,
            //    DocumentId = decisionSecretarHand.Id
            //};
            //builder.Entity<DocumentUser>().HasData(documentUser);

            //lastDocUser = documentUser;

            //ZemstvoUserPosition userSecretarHand = new ZemstvoUserPosition
            //{
            //    Id = 1,
            //    DTCUTC = new DateTime(2019, 11, 26),
            //    PositionId = secretarHand.Id,
            //    UserCreatorId = admin.Id,
            //    UserId = admin.Id,
            //    DocumentElectionId = decisionSecretarHand.Id,
            //    ZemstvoId = sz.Id
            //};
            //builder.Entity<ZemstvoUserPosition>().HasData(userSecretarHand);

            //if (true)
            //{
            //    TestData(builder, admin, lastDocument, lastDocUser, sz);
            //}
        }


        //private static void TestData(ModelBuilder builder, User lastUser, Document lastDoc, DocumentUser lastDocUser, Zemstvo lastZemstvo)
        //{
        //    var user1result = AddUser(builder, (lastUser, lastDoc, lastDocUser), lastZemstvo, "Иван", "Иванов", "Иванович", "ivanov@ya.ru", "11111111111");
        //    var user2result = AddUser(builder, user1result, lastZemstvo, "Пётр", "Петров", "Петрович", "petrov@yandex.ru", "22222222222");
        //    var user3result = AddUser(builder, user2result, lastZemstvo, "Сидор", "Сидоров", "Сидорович", "sidorov@yandex.ru", "33333333333");
        //    var user4result = AddUser(builder, user3result, lastZemstvo, "Павел", "Павлов", "Павлович", "pavlov@yandex.ru", "44444444444");
        //    var user5result = AddUser(builder, user4result, lastZemstvo, "Дмитрий", "Дмитриев", "Дмитриевич", "dmitry@yandex.ru", "55555555555");
        //    var ten1Result = AddTen(builder, (null, null), lastZemstvo, 2, null);
        //    var ten2Result = AddTen(builder, ten1Result, lastZemstvo, 1, ten1Result.lastTen.Id, lastUser, user1result.user);
        //    var ten3Result = AddTen(builder, ten2Result, lastZemstvo, 1, ten1Result.lastTen.Id, user2result.user, user3result.user);
        //    var ten4Result = AddTen(builder, ten3Result, lastZemstvo, 1, ten1Result.lastTen.Id, user4result.user, user5result.user);
        //    var resultChooseDelegateTen2 = ChooseDelegateInTen(builder, ten2Result.lastTen, lastZemstvo, (null, null, null, user5result.lastDoc, null, user5result.lastDocUser, null, ten4Result.lastUserTen), true, lastUser, user1result.user);
        //    var resultChooseDelegateTen3 = ChooseDelegateInTen(builder, ten3Result.lastTen, lastZemstvo, resultChooseDelegateTen2, true, user2result.user, user3result.user);
        //    var resultChooseDelegateTen4 = ChooseDelegateInTen(builder, ten4Result.lastTen, lastZemstvo, resultChooseDelegateTen3, true, user4result.user, user5result.user);
        //    return;
        //}

        //static (Ten lastTen, UserTen lastUserTen) AddTen(ModelBuilder builder, (Ten lastTen, UserTen lastUserTen) args, Zemstvo zemstvo, byte circle, Guid? parentTenId, params User[] users)
        //{
        //    Ten ten1 = new Ten
        //    {
        //        Id = (args.lastTen?.Id ?? 0) + 1,
        //        Circle = circle,
        //        CreatorId = users?.FirstOrDefault()?.Id ?? 1,
        //        DTCUTC = DateTime.Now,
        //        ParentTenId = parentTenId,
        //        ZemstvoId = zemstvo.Id
        //    };
        //    builder.Entity<Ten>().HasData(ten1);

        //    var userTenCounter = (args.lastUserTen?.Id ?? 0) + 1;
        //    UserTen lastUserTen = null;
        //    foreach (var user in users)
        //    {
        //        UserTen userTen = new UserTen
        //        {
        //            Id = userTenCounter++,
        //            TenId = (args.lastTen?.Id ?? 0) + 1,
        //            UserId = user.Id,
        //            BasisEntranceDocumentId = 1
        //        };
        //        builder.Entity<UserTen>().HasData(userTen);
        //        lastUserTen = userTen;
        //    }


        //    return (ten1, lastUserTen);
        //}

        //static (Question lastQuestion, QuestionRepeat lastQR, QuestionRepeatAnswer lastQRAnswer, Document lastDocument,
        //    ProtocolQuestionRepeat lastProtocolQR, DocumentUser lastDocUser, DocumentDecision lastDocDecision, UserTen lastUserTen)
        //    ChooseDelegateInTen(ModelBuilder builder, Ten ten, Zemstvo zemstvo, 
        //    (Question lastQuestion, QuestionRepeat lastQR, QuestionRepeatAnswer lastQRAnswer, Document lastDocument, 
        //        ProtocolQuestionRepeat lastProtocolQR, DocumentUser lastDocUser, DocumentDecision lastDocDecision, UserTen lastUserTen) args, 
        //    bool confirmProtocol, params User[] users)
        //{
        //    Question question = new Question
        //    {
        //        Id = (args.lastQuestion?.Id ?? 0) + 1,
        //        CandidateDestination = EnumQuestionCandidateDestination.Delegate,
        //        DelegatTenId = ten.Id,
        //        DTCUTC = DateTime.UtcNow,
        //        InitiatorId = users.FirstOrDefault().Id,
        //        Type = EnumQuestionType.ChoicePerson,
        //        QuestionCandidateSource = EnumQuestionCandidateSource.Ten,
        //        ZemstvoId = zemstvo.Id
        //    };
        //    builder.Entity<Question>().HasData(question);

        //    QuestionRepeat questionRepeat = new QuestionRepeat
        //    {
        //        Id = (args.lastQR?.Id ?? 0) + 1,
        //        DTCUTC = DateTimeOffset.UtcNow,
        //        Essence = "Выбор делегата в следующий круг",
        //        IsLast = true,
        //        MaxChoiceVariantCount = 1,
        //        QuestionId = question.Id,
        //        Reason = "Create question",
        //        SequenceNumber = 1,
        //        VariantCount = 1,
        //        UpdaterId = users.FirstOrDefault().Id
        //    };
        //    builder.Entity<QuestionRepeat>().HasData(questionRepeat);

        //    int QRAnswerCounter = (args.lastQRAnswer?.Id ?? 0) + 1;
        //    QuestionRepeatAnswer lastQRAnswer = null;
        //    foreach (var user in users)
        //    {
        //        QuestionRepeatAnswer qrAnswer = new QuestionRepeatAnswer
        //        {
        //            Id = QRAnswerCounter++,
        //            CandidatId = user.Id,
        //            QuestionRepeatId = questionRepeat.Id
        //        };
        //        builder.Entity<QuestionRepeatAnswer>().HasData(qrAnswer);
        //        lastQRAnswer = qrAnswer;
        //    }

        //    Document protocolDelegateInTen = new Document
        //    {
        //        Id = (args.lastDocument?.Id ?? 0) + 1,
        //        DTCUTC = DateTimeOffset.UtcNow,
        //        FactDateDocument = DateTime.UtcNow,
        //        MeetingQuorum = 2,
        //        ProtocolStatus = EnumTenProtocolStatus.Ended,
        //        ProtocolTenId = ten.Id,
        //        Status = confirmProtocol ? EnumDocumentStatus.Confirmation : EnumDocumentStatus.ForCheck,
        //        Type = EnumDocumentType.ProtocolTen,
        //        UserCreatorId = users.FirstOrDefault().Id,
        //        DTCUTCAccept = DateTimeOffset.UtcNow,
        //        VotingQuorum = 2,
        //        ZemstvoId = zemstvo.Id,
        //        UserPositionAcceptorId = 1,
        //        UserPositionCheckerId = confirmProtocol ? (int?)1 : null,
        //        DTCUTCCheck = DateTime.Now
        //    };
        //    builder.Entity<Document>().HasData(protocolDelegateInTen);


        //    ProtocolQuestionRepeat protocolQuestionRepeatDelegateInTen = new ProtocolQuestionRepeat
        //    {
        //        Id = (args.lastProtocolQR?.Id ?? 0) + 1,
        //        ProtocolId = protocolDelegateInTen.Id,
        //        QuestionRepeatId = questionRepeat.Id
        //    };
        //    builder.Entity<ProtocolQuestionRepeat>().HasData(protocolQuestionRepeatDelegateInTen);

        //    int docUserCounter = (args.lastDocUser?.Id ?? 0) + 1;
        //    int docDecisionCounter = (args.lastDocDecision?.Id ?? 0) + 1;
        //    DocumentUser lastDocumentuser = null;
        //    DocumentDecision lastDocumentDecision = null;

        //    foreach (var userTen in users)
        //    {
        //        DocumentUser documentUser = new DocumentUser
        //        {
        //            Id = docUserCounter++,
        //            UserId = userTen.Id,
        //            DocumentId = protocolDelegateInTen.Id,
        //            AttendsTheMeeting = true
        //        };
        //        builder.Entity<DocumentUser>().HasData(documentUser);
        //        lastDocumentuser = documentUser;

        //        DocumentDecision decisionDelegateInTen = new DocumentDecision
        //        {
        //            Id = docDecisionCounter++,
        //            QuestionRepeatAnswerId = (args.lastQRAnswer?.Id ?? 0) + 1,
        //            DocumentUserId = documentUser.Id,
        //            ProtocolQuestionRepeatId = protocolQuestionRepeatDelegateInTen.Id,
        //            RealDecideUserId = 1
        //        };
        //        builder.Entity<DocumentDecision>().HasData(decisionDelegateInTen);

        //        lastDocumentDecision = decisionDelegateInTen;
        //    }

        //    UserTen lastUserTen = args.lastUserTen;

        //    if (confirmProtocol)
        //    {
        //        var delegatUserTen = new UserTen
        //        {
        //            Id = (args.lastUserTen?.Id ?? 0) + 1,
        //            TenId = ten.ParentTenId.Value,
        //            UserId = users.FirstOrDefault().Id,
        //            BasisEntranceDocumentId = protocolDelegateInTen.Id,
        //        };
        //        builder.Entity<UserTen>().HasData(delegatUserTen);
        //        lastUserTen = delegatUserTen;
        //    }

        //    return (question, questionRepeat, lastQRAnswer, protocolDelegateInTen, protocolQuestionRepeatDelegateInTen, lastDocumentuser, lastDocumentDecision, lastUserTen);
        //}

        //static (User user, Document lastDoc, DocumentUser lastDocUser) AddUser(ModelBuilder builder,
        //    (User lastUser, Document lastDoc, DocumentUser lastDocUser) lastIds, Zemstvo zemstvo,
        //    string firstName, string secondName, string patronym, string email, string phone)
        //{
        //    User user = new User
        //    {
        //        Id = (lastIds.lastUser?.Id ?? 0)  + 1,
        //        FirstName = firstName,
        //        SecondName = secondName,
        //        Patronym = patronym,
        //        Email = email,
        //        NormalizedUserName = phone,
        //        NormalizedEmail = email.ToUpper(),
        //        PasswordHash = "AQAAAAEAACcQAAAAEEUFx0jIuCPgIwq13eLmj/7r07Q2A2dMO02o+XPlxcRU4xFeHxOZx3Dq4L3Icf+KVw==",// 1234Qwerty
        //        SecurityStamp = "PTFHCDZLW4JTLAAVMJMRLUOLQDJCV6I3",
        //        ConcurrencyStamp = "e5b1a441-71e0-40f2-b1ba-fc035ea7c54d",
        //        PhoneNumber = phone,
        //        UserName = phone,
        //        Gender = true
        //    };
        //    builder.Entity<User>().HasData(user);

        //    Document statementUser = new Document
        //    {
        //        Id = (lastIds.lastDoc?.Id ?? 0) + 1,
        //        DTCUTC = DateTime.Now,
        //        FactDateDocument = DateTime.Now,
        //        Status = EnumDocumentStatus.Confirmation,
        //        Type = EnumDocumentType.StatementInput,
        //        UserCreatorId = user.Id,
        //        ZemstvoId = zemstvo.Id,
        //        UserPositionAcceptorId = 1,
        //        UserPositionCheckerId = 1,
        //        DTCUTCCheck = DateTime.Now
        //    };
        //    builder.Entity<Document>().HasData(statementUser);

        //    DocumentUser documentUserStatement = new DocumentUser
        //    {
        //        Id = (lastIds.lastDocUser?.Id ?? 0) + 1,
        //        DocumentId = statementUser.Id,
        //        UserId = user.Id
        //    };
        //    builder.Entity<DocumentUser>().HasData(documentUserStatement);

        //    return (user, statementUser, documentUserStatement);
        //}
    }
}
